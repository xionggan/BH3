using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour {

    public WeaponTrail myTrail;
    [HideInInspector]
    public Animator animator;
    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

    void Start()
    {
        animator = GetComponent<Animator>();
        // 默认没有拖尾效果
        myTrail.SetTime(0.0f, 0.0f, 1.0f);
    }

    void Update()
    {
        int curAnimaTag = animator.GetCurrentAnimatorStateInfo(0).tagHash;
        int curTransitionNameHash = animator.GetAnimatorTransitionInfo(0).userNameHash;
        bool isInTransition = animator.IsInTransition(0);

        if ((curAnimaTag == AnimaConstant.TagAtk && !isInTransition) ||
            curTransitionNameHash == AnimaConstant.TransitionToAttack)
        {
            //设置拖尾时长
            myTrail.SetTime(0.1f, 0.0f, 1.0f);
            //开始进行拖尾
            myTrail.StartTrail(0.1f, 0.1f);
        }
        else
        {
            myTrail.ClearTrail();
        }
    }

    void LateUpdate()
    {

        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0)
        {
            while (tempT < t)
            {
                tempT += animationIncrement;

                if (myTrail.time > 0)
                {
                    myTrail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    myTrail.ClearTrail();
                }
            }

            tempT -= t;

            if (myTrail.time > 0)
            {
                myTrail.UpdateTrail(Time.time, t);
            }
        }
    }
}
