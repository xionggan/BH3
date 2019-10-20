using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterManager {

    public PlayerController nextHero;

    public AudioManager audioManager;

    public ShowShadowTail shadowTailController;

    private float atkBtnPressTime;

    private float standByTime;

    [HideInInspector]
    private Transform lockTarget;

    public Transform LockTarget
    {
        get { return lockTarget; }
        set
        { 
            lockTarget = value;
            uiManager.enemyController = lockTarget? lockTarget.GetComponent<EnemyController>() : null;
            uiManager.UpdateEnemyInfo();
        }
    }

	// Update is called once per frame
    public override void Update()
    {
        float xDelta = ETCInput.GetAxis("Horizontal");

        float zDelta = ETCInput.GetAxis("Vertical");

        Camera camera = Camera.main;

        Vector3 moveDir = xDelta * camera.transform.right + zDelta * camera.transform.forward;

        moveDir.y = 0;

        float speed = moveDir.magnitude;

        moveDir.Normalize();

        bool isAtkKeyDown = Input.GetKeyDown(KeyCode.J) || ETCInput.GetButtonDown("ButtonAtk");

        bool isAtkKeyPress = Input.GetKey(KeyCode.J) || ETCInput.GetButton("ButtonAtk");

        if (isAtkKeyPress)
        {
            atkBtnPressTime += Time.unscaledDeltaTime;
        }
        else
        {
            atkBtnPressTime = 0;
        }

        bool isAtkKeyLongPress = atkBtnPressTime > 0.1f;

        bool isEvadeKeyDown = Input.GetKeyDown(KeyCode.K) || ETCInput.GetButton("ButtonEvade");

        bool isSwitchKeyDown = Input.GetKeyDown(KeyCode.L) || ETCInput.GetButton("ButtonSwitch");

        int curAnimaTag = animator.GetCurrentAnimatorStateInfo(0).tagHash;

        bool isInTransition = animator.IsInTransition(0);

        int curTransitionNameHash = animator.GetAnimatorTransitionInfo(0).userNameHash;

        //Movement
        if (speed > 0.01f)
        {
            animator.SetFloat(AnimaConstant.Speed, speed);

            if ((curAnimaTag == AnimaConstant.TagMovement
                || curTransitionNameHash == AnimaConstant.TransitionToMovement))
            {
                transform.forward = moveDir;
            }
        }
        else
        {
            animator.SetFloat(AnimaConstant.Speed, 0);
        }

        //lock
        if (curAnimaTag == AnimaConstant.TagAtk || curTransitionNameHash == AnimaConstant.TransitionToAttack)
        {
            if (LockTarget == null 
                || curTransitionNameHash == AnimaConstant.TransitionToEvade
                || curTransitionNameHash == AnimaConstant.TransitionToMovement)
            {
                if (speed > 0.01f && isInTransition)
                {
                    transform.forward = moveDir;
                }
            }
            else
            {
                transform.forward = LockTarget.position - transform.position;
            }
        }

        //Attack
        if (isAtkKeyDown)
        {
            if (LockTarget == null)
            {
                Vector3 intentDir = speed > 0.01f ? moveDir : transform.forward;

                LockTarget = findCloseEnemyByDir(intentDir);
            }

            if (LockTarget != null && speed > 0.01f)
            {
                LockTarget = findCloseEnemyByDir(moveDir);
            }

            animator.SetTrigger(AnimaConstant.Atk);
        }

        animator.SetBool(AnimaConstant.ChargeAtk, isAtkKeyLongPress);

        //Evade
        if (isEvadeKeyDown && curAnimaTag != AnimaConstant.TagEvade && !isInTransition)
        {
            animator.SetTrigger(AnimaConstant.Evade);
            animator.ResetTrigger(AnimaConstant.Atk);
            audioManager.Play(0, false);
        }

        //Idle
        if (curAnimaTag == AnimaConstant.TagStandBy)
        {
            standByTime += Time.unscaledDeltaTime;
        }
        else
        {
            standByTime = 0;
            animator.SetInteger(AnimaConstant.Idle, 0);
        }

        if (standByTime > 5)
        {
            animator.SetInteger(AnimaConstant.Idle, Random.Range(1, 4));
        }

        //Switch
        if (isSwitchKeyDown && Hp > 0 && curAnimaTag != AnimaConstant.TagSwitch && !isInTransition)
        {
            animator.SetTrigger(AnimaConstant.SwitchOut);
            StartCoroutine(DelayDeActive());
        }

        nextHero.transform.position = transform.position;

        if (shadowTailController) 
        {
            if (curAnimaTag == AnimaConstant.TagEvade
                || curTransitionNameHash == AnimaConstant.TransitionToEvade)
            {
                shadowTailController.createLength = 0.2f;
            }
            else
            {
                shadowTailController.createLength = 0;
            }
        }
	}

    public override void OnHpChange()
    {
        base.OnHpChange();
        uiManager.UpdatePlayerInfo();
    }

    public void OnEnable()
    {
        Camera.main.GetComponent<CameraController>().player = transform;
        uiManager.playerController = this;
        uiManager.UpdatePlayerInfo();
    }

    private IEnumerator DelayDeActive()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        nextHero.gameObject.SetActive(true);
        yield return null;
        nextHero.animator.updateMode = animator.updateMode;
        nextHero.transform.rotation = transform.rotation;
        nextHero.LockTarget = LockTarget;
        nextHero.animator.SetTrigger(AnimaConstant.SwitchIn);
        nextHero.audioManager.Play(0, false);
        yield return new WaitForSecondsRealtime(0.1f);
        gameObject.SetActive(false);
    }

    private Transform findCloseEnemyByDir(Vector3 intentDir)
    {
        Transform result = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Enemy"));

        float angle = 180;

        foreach (Collider collider in colliders)
        {
            Transform enemy = collider.gameObject.transform;

            Vector3 enemyDir = (enemy.position - transform.position).normalized;

            float deltaAngle = Mathf.Acos(Vector3.Dot(intentDir, enemyDir));

            if (deltaAngle < angle)
            {
                angle = deltaAngle;
                result = enemy;
            }
        }

        return result;
    }

    public void AE_LeftStop()
    {
        animator.SetBool(AnimaConstant.IsLeftStop, true);
    }

    public void AE_RightStop()
    {
        animator.SetBool(AnimaConstant.IsLeftStop, false);
    }

    public virtual void AE_DamagePenalty(DamageType damageType)
    {
        audioManager.Play(1, false);

        if (atkAngleRange > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, atkLength, LayerMask.GetMask("Enemy"));

            foreach (Collider collider in colliders)
            {
                Transform enemy = collider.gameObject.transform;

                Vector3 atkDir = (enemy.position - transform.position).normalized;

                float atkAngle = Mathf.Acos(Vector3.Dot(transform.forward, atkDir)) * Mathf.Rad2Deg;

                if (atkAngle < atkAngleRange / 2)
                {
                    RaycastHit hitInfo;

                    Physics.Raycast(transform.position + Vector3.up * 0.5f, atkDir, out hitInfo, atkLength);

                    if (hitInfo.collider != collider)
                    {
                        continue;
                    }

                    EnemyController enemyController = enemy.GetComponent<EnemyController>();

                    if(enemyController.Hp <= 0)
                    {
                        continue;
                    }

                    enemyController.TakeDamage(damageType);

                    if(LockTarget == null)
                    {
                        LockTarget = enemy;
                    }
                }
            }
        }
        else
        {
            RaycastHit hitInfo;

            Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hitInfo, atkLength, LayerMask.GetMask("Enemy"));

            if (hitInfo.collider)
            {
                EnemyController enemyController = hitInfo.collider.gameObject.GetComponent<EnemyController>();

                if (enemyController.Hp <= 0)
                {
                    return;
                }

                 enemyController.TakeDamage(damageType);

                if (LockTarget == null)
                {
                    LockTarget = hitInfo.collider.transform;
                }
            }
        }
    }
}
