using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterManager : MonoBehaviour {

    private int hp;

    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            OnHpChange();
        }
    }

    public int maxHp;

    private int shieldCount;

    public int ShieldCount
    {
        get { return shieldCount; }
        set
        {
            shieldCount = value;
            OnShieldChange();
        }
    }

    public int maxShieldCount;

    public float atkLength;

    public float atkAngleRange;

    public float damageFactor = 1;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public CombatUIManager uiManager;


    public enum DamageType
    {
        DT_L = 0,
        DT_H,
        DT_Stun,
        DT_ThrowDown,
        DT_ThrowAway,
    }


    public virtual void OnHpChange()
    {
    
    }

    public virtual void OnShieldChange()
    {

    }

    public virtual void Awake()
    {
        uiManager = GameObject.Find("UIRoot").GetComponent<CombatUIManager>();
    }

	// Use this for initialization
    public virtual void Start()
    {
        ShieldCount = maxShieldCount;
        animator = GetComponent<Animator>();
        Hp = maxHp;
	}
	
	// Update is called once per frame
    public virtual void Update()
    {
		
	}

    public int GetDamageCountByType(DamageType damageType)
    {
        int damage = 0;

        switch (damageType)
        {
            case DamageType.DT_L:
                damage = 5;
                break;
            case DamageType.DT_H:
                damage = 10;
                break;
            case DamageType.DT_Stun:
                damage = 15;
                break;
            case DamageType.DT_ThrowDown:
                damage = 20;
                break;
            case DamageType.DT_ThrowAway:
                damage = 25;
                break;
        }

        return (int)(damage * damageFactor);
    }

    public virtual void TakeDamage(DamageType damageType)
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine(ChangeTimeScale(0,0.05f));
        }

        Camera.main.GetComponent<CameraController>().StartShake();

        int damage = GetDamageCountByType(damageType);

        if (ShieldCount > 0)
        {
            ShieldCount -= damage;

            if (ShieldCount <= 0)
            {
                ShieldCount = 0;
                BreakShield();
            }
        }
        else
        {
            Hp -= damage;

            if (this is EnemyController)
            {
                int curAnimaTag = animator.GetCurrentAnimatorStateInfo(0).tagHash;

                int curTransitionNameHash = animator.GetAnimatorTransitionInfo(0).userNameHash;

                if (curAnimaTag != AnimaConstant.TagAtk && curTransitionNameHash != AnimaConstant.TransitionToAttack)
                {
                    animator.SetTrigger(AnimaConstant.Hit);
                }
            }
            else 
            {
                animator.SetTrigger(AnimaConstant.Hit);
            }

            if (Hp <= 0)
            {
                Hp = 0;
                Death();
            }
            else
            {
                animator.SetInteger(AnimaConstant.DamageType, (int)damageType);
            }
        }
    }

    public virtual void BreakShield()
    {

    }

    public virtual void Death()
    {
        animator.SetBool(AnimaConstant.Death, true);
    }


    public IEnumerator ChangeTimeScale(float timeScale, float duration)
    {
        float originScale = Time.timeScale;
        float originFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = timeScale * originFixedDeltaTime;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = originScale;
        Time.fixedDeltaTime = originFixedDeltaTime;
    }

    public IEnumerator ChangeTimeExceptPlayer(float timeScale, float duration)
    {

        float originScale = Time.timeScale;
        float originFixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = timeScale;
        Time.fixedDeltaTime = timeScale * originFixedDeltaTime;

        DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x,
            new Color(0.1f, 0.1f, 0.3f),0.5f);

        yield return new WaitForSecondsRealtime(0.6f);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            playerController.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originScale;
        Time.fixedDeltaTime = originFixedDeltaTime;

        DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x,
            new Color(0.2f, 0.2f, 0.2f), 0.5f);

        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            playerController.animator.updateMode = AnimatorUpdateMode.Normal;
        }
    } 
}
