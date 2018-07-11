using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character {

    protected float health, special, defence, strength, moveSpeed, punchDamage, kickDamage, special1Damage, special2Damage, ultraDamage;
    protected string name;

    /// <summary>
    /// This should be implemented to calculate damage taken based on characters special attributes, strength,
    /// defence, and a small bit of chance. 
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    abstract public float CalculateDamage(float damage);

    public float GetHealth()
    {
        return health;
    }

    public float GetDefence()
    {
        return defence;
    }

    public float GetStrength()
    {
        return strength;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public abstract void movePunch();
    public abstract void moveKick();
    public abstract void special1();
    public abstract void special2();
    public abstract void ultra();

    public abstract AnimatorOverrideController GetAnimationController();


    public string GetName()
    {
        return name;
    }

    public float GetSpecial()
    {
        return special;
    }

    public enum Characters
    {
        BJORN,
        BITCOINBOY,
        DOGE,
        MONERO
    }


    public static Character Get(Characters c)
    {
        switch (c)
        {
            case Characters.BJORN:
                return new Bjorn();
            case Characters.DOGE:
                return new Doge();
            default:
                return new Doge();
        }
    }
}
