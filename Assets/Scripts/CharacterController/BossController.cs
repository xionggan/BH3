using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public float rollDamageIntervalTime = 0.2f;

    private float curDeltaTime;

    [HideInInspector]
    public bool isRolling;

    public void AE_RollStart()
    {
        isRolling = true;
    }

    public void AE_RollEnd()
    {
        isRolling = false;
    }

    public override void BreakShield()
    {
        base.BreakShield();
        isRolling = false;
    }

    public void AE_AOEDamagePenalty()
    {
        DrawCircle(transform,transform.position, 7f);
        float originAtkAngleRange = atkAngleRange;
        float originAtkRange = atkLength;
        atkAngleRange = 360;
        atkLength = 7f;
        AE_DamagePenalty(DamageType.DT_H);
        atkAngleRange = originAtkAngleRange;
        atkLength = originAtkRange;
    }


    public void AE_LineDamagePenalty()
    {
        DrawLine(transform,50);
        float originAtkAngleRange = atkAngleRange;
        float originAtkRange = atkLength;
        atkAngleRange = 10;
        atkLength = 50;
        AE_DamagePenalty(DamageType.DT_H);
        atkAngleRange = originAtkAngleRange;
        atkLength = originAtkRange;
    }

    private void DrawLine(Transform t, float length)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;

        Vector3 startPos = t.position + Vector3.up;
        Vector3 endPos = startPos + t.forward * length;

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);

        Destroy(lr, 1.2f);
    }

    private void DrawCircle(Transform t,Vector3 center,float radius)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;

        int pointAmount = 100;
        float eachAngle = 360 / pointAmount;
        Vector3 forward = t.forward;
        lr.positionCount = (pointAmount + 1);

        for (int i = 0; i <= pointAmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius + center;
            lr.SetPosition(i, pos);
        }
        Destroy(lr, 1.2f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isRolling)
        {
            AE_DamagePenalty(DamageType.DT_L);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isRolling)
        {
            curDeltaTime += Time.deltaTime;

            if (curDeltaTime > rollDamageIntervalTime)
            {
                AE_DamagePenalty(DamageType.DT_L);
                curDeltaTime = 0;
            }
        }
    }

}
