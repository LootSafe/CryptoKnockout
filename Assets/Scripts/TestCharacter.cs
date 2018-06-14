using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : Character{

    public TestCharacter()
    {
        this.health = 10;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 1;
        //this.name = "Bitcoin Boiiiii";
    }

    public override float CalculateDamage(float damage)
    {
        return 1;
    }
}
