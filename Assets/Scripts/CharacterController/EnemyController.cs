using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyController : CharacterManager
{
    public float weakTime = 8;

    private float curLeftWeakTime;

    public override void Update()
    {
        base.Update();
        if (curLeftWeakTime > 0)
        {
            curLeftWeakTime -= Time.deltaTime;

            animator.SetFloat(AnimaConstant.WeakTime, curLeftWeakTime);

            if (curLeftWeakTime <= 0)
            {
                ShieldCount = maxShieldCount;
            }
        }
    }

    public override void OnHpChange()
    {
        base.OnHpChange();
        if (this == uiManager.enemyController)
        {
            uiManager.UpdateEnemyInfo();
        }
    }

    public override void OnShieldChange()
    {
        base.OnHpChange();
        if (this == uiManager.enemyController)
        {
            uiManager.UpdateEnemyInfo();
        }
    }

    public override void BreakShield()
    {
        animator.SetTrigger(AnimaConstant.BreakShield);
        animator.SetFloat(AnimaConstant.WeakTime, weakTime);
        curLeftWeakTime = weakTime;
    }

    public override void Death()
    {
        animator.SetBool(AnimaConstant.Death, true);
        Destroy(gameObject.GetComponent<BehaviorTree>());
        GameObject.Destroy(gameObject,5);
        if (uiManager.playerController && this == uiManager.playerController.LockTarget)
        {
            uiManager.playerController.LockTarget = null;
        }

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        bool isLastLiveEnemy = true;

        for (int i = 0; i < enemys.Length;i++ )
        {
            EnemyController enemyController = enemys[i].GetComponent<EnemyController>();

            if (enemyController.Hp > 0)
            {
                isLastLiveEnemy = false;
            }
        }

        if (isLastLiveEnemy)
        {
            uiManager.PlayVictoryAnima(3);
        }
    }

    public void AE_DamagePenalty(DamageType damageType)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, atkLength, LayerMask.GetMask("Player"));

        foreach (Collider collider in colliders)
        {
            Transform player = collider.gameObject.transform;

            Vector3 atkDir = (player.position - transform.position).normalized;

            float atkAngle = Mathf.Acos(Vector3.Dot(transform.forward, atkDir)) * Mathf.Rad2Deg;

            if (atkAngle < atkAngleRange / 2)
            {
                RaycastHit hitInfo;

                Physics.Raycast(transform.position + Vector3.up * 0.5f, atkDir, out hitInfo, atkLength);

                if (hitInfo.collider != collider)
                {
                    continue;
                }

                PlayerController playerController = player.GetComponent<PlayerController>();

                if (playerController.Hp <= 0)
                {
                    continue;
                }

                int curAnimaTag = playerController.animator.GetCurrentAnimatorStateInfo(0).tagHash;

                int curTransitionNameHash = playerController.animator.GetAnimatorTransitionInfo(0).userNameHash;

                if (curTransitionNameHash == AnimaConstant.TransitionToEvade || curAnimaTag == AnimaConstant.TagEvade)
                {
                    if(Time.timeScale == 1)
                    {
                        StartCoroutine(ChangeTimeExceptPlayer(0.2f, 5f));
                        if (playerController as KianaController)
                        {
                            //黑洞
                        }
                        else
                        {
                            playerController.animator.SetTrigger(AnimaConstant.EvadeAtk);
                        }
                    }
                }
                else
                {
                    playerController.TakeDamage(damageType);
                }
            }
        }
    }
}
