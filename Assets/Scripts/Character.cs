using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected float health, defence, strength, moveSpeed;

    public float CalculateDamage()
    {
        return 0f;
    }

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
}
