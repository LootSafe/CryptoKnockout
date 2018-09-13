using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bjorn : Character {

    public Bjorn()
    {
        this.health = 1000;
        this.strength = 35;
        this.defence = 8;
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

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/Bjorn", typeof(AnimatorOverrideController));
        return p;
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
        player.UseSuper();
    }
}
