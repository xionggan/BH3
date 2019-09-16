using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaController : PlayerController {

    public ParticleSystem leftGunFire;

    public ParticleSystem rightGunFire;

    private int attack01 = Animator.StringToHash("Avatar_Kiana_C1_Ani_Attack_Gun_01");
    private int attack02 = Animator.StringToHash("Avatar_Kiana_C1_Ani_Attack_Gun_02");
    private int attack03 = Animator.StringToHash("Avatar_Kiana_C1_Ani_Attack_Gun_03");
    private int attack04_01 = Animator.StringToHash("Avatar_Kiana_C1_Ani_Attack_Gun_04_01");
    private int attack04_02 = Animator.StringToHash("Avatar_Kiana_C1_Ani_Attack_Gun_04_02");

    public override void AE_DamagePenalty(DamageType damageType)
    {
        base.AE_DamagePenalty(damageType);

        int curAttack = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

        if (curAttack == attack01)
        {
            rightGunFire.Play();
        }
        else if (curAttack == attack02)
        {
            leftGunFire.Play();
        }
        else if (curAttack == attack03)
        {
            rightGunFire.Play();
        }
        else if (curAttack == attack04_01)
        {
            rightGunFire.Play();
            leftGunFire.Play();
        }
        else if (curAttack == attack04_02)
        {
            rightGunFire.Play();
            leftGunFire.Play();
        }
    }
}
