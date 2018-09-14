using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthBot : Character {

    public EthBot()
    {
        this.health = 90;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 10;
        this.name = "EthBot";

        this.hitWords = new List<string>();
        this.hitWords.Add("ZZZAAAP!!");
        this.hitWords.Add("ZIIINGGGG!!");
        this.hitWords.Add("CLASSHHH!!");
        this.hitWords.Add("CLAANK!!");
        this.hitWords.Add("CLIINK!!");
        this.hitWords.Add("CACHUNK!!!");
    }

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/EthBot", typeof(AnimatorOverrideController));
        
        return p;
    }

    public override void MoveSpecial1()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveSpecial2()
    {
        throw new System.NotImplementedException();
    }

}
