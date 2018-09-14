using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCBoy : Character {

    public BTCBoy()
    {
        this.health = 90;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 10;
        this.name = "Bitcoin Boy";

        this.hitWords = new List<string>();
        this.hitWords.Add("POOOWW!!");
        this.hitWords.Add("BIIINGG!");
        this.hitWords.Add("CA-CHING!");
        this.hitWords.Add("KAAPOW!!!");
        this.hitWords.Add("CLIINK!!");
    }

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/BTCBoy", typeof(AnimatorOverrideController));
        
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
