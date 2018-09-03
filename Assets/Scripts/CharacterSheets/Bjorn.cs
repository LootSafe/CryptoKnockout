using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bjorn : Character {

    public Bjorn()
    {
        this.health = 90000;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 10;
        this.name = "Bjorn";

        this.hitWords = new List<string>();
        this.hitWords.Add("BAAAM!!");
        this.hitWords.Add("POOOWW!!");
        this.hitWords.Add("ROOAR!!");
        this.hitWords.Add("ZAAAM!!!");
        this.hitWords.Add("KAABOOM!!");
        this.hitWords.Add("KAAPOOWW!");
    }

    public override float CalculateDamage(float damage)
    {
        return damage;
    }

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/Bjorn", typeof(AnimatorOverrideController));
        return p;
    }

    public override void MoveKick()
    {
        player.foot.GetComponent<PlayerDamage>().TriggerEnable();
    }

    public override void MovePunch()
    {
        player.fist.GetComponent<PlayerDamage>().TriggerEnable();
    }

    public override void MoveBlock()
    {
        
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
