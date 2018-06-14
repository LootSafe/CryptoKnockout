using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : Character{

    public TestCharacter()
    {
        this.health = 50;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 0.1f;
        //this.name = "Bitcoin Boiiiii";
    }

    public override float CalculateDamage(float damage)
    {
        return damage;
    }
}
