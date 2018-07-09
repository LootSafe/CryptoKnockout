using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bjorn : Character {

    public Bjorn()
    {
        this.health = 1000;
        this.strength = 10;
        this.defence = 10;
        this.moveSpeed = 0.1f;
        this.name = "Bjorn";
    }

    public override float CalculateDamage(float damage)
    {
        return damage;
    }

}
