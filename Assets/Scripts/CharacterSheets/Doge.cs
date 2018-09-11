using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : Character {

    public Doge()
    {
        this.health = 500;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 10;
        this.name = "Doge";

        this.hitWords = new List<string>();
        this.hitWords.Add("BANG!!");
        this.hitWords.Add("WHAAAM!!");
        this.hitWords.Add("MUCH WOW!!");
        this.hitWords.Add("FLIM!!");
        this.hitWords.Add("FLAAM!!");
        this.hitWords.Add("WOOOOFFF!!");
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
        player.IsAttacking();
        player.foot.GetComponent<PlayerDamage>().TriggerEnable();
    }

    public override void MoveBlock()
    {
        
    }
    public override void MovePunch()
    {
        player.IsAttacking();
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
