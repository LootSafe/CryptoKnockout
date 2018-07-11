using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : Character {

    public Doge()
    {
        this.health = 500;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 0.1f;
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
        throw new System.NotImplementedException();
    }

    public override void MovePunch()
    {
        throw new System.NotImplementedException();
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
