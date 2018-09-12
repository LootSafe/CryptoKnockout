using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : Character {

    public Doge()
    {
        this.health = 300;
        this.strength = 45;
        this.defence = 12;
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


    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/Doge", typeof(AnimatorOverrideController));
        
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
