using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monero : Character {

    public Monero()
    {
        this.name = "Monero";
        this.hitWords = new List<string>();
        this.hitWords.Add("SWIISH!!");
        this.hitWords.Add("SLAASH!!");
        this.hitWords.Add("SLIIING!!");
        this.hitWords.Add("SLAAAP!!");
        this.hitWords.Add("SLIIP!!");
        this.hitWords.Add("SWOOOP!!!");
    }

    public override AnimatorOverrideController GetAnimationController()
    {
        AnimatorOverrideController p = (AnimatorOverrideController)Resources.Load("Animator/Characters/EthBot", typeof(AnimatorOverrideController));
        
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
