using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinBoy : Character {

    public BitcoinBoy()
    {
        this.health = 90;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 10;
        this.name = "Doge";
    }

    public override float CalculateDamage(float damage)
    {
        return damage;
    }

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/Doge", typeof(AnimatorOverrideController));
        
        return p;
    }

    public override void MoveKick()
    {
        player.foot.GetComponent<PlayerDamage>().TriggerEnable();
    }

    public override void MoveBlock()
    {
        
    }
    public override void MovePunch()
    {
        player.fist.GetComponent<PlayerDamage>().TriggerEnable();
    }

    public override void MoveSpecial1()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveSpecial2()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveUltra()
    {
        throw new System.NotImplementedException();
    }
}
