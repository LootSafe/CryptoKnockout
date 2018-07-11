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

    public override void moveKick()
    {
        throw new System.NotImplementedException();
    }

    public override void movePunch()
    {
        throw new System.NotImplementedException();
    }

    public override void special1()
    {
        throw new System.NotImplementedException();
    }

    public override void special2()
    {
        throw new System.NotImplementedException();
    }

    public override void ultra()
    {
        throw new System.NotImplementedException();
    }
}
