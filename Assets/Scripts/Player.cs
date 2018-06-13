using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private static Player instance;
    
    public float maxHealth;
    private float health;
    public float lives;
    private bool alive;
    
    void Start()
    {
        instance = this;
    }

    public void TakeDamage(float damage)
    {

    }
	
    public bool IsAlive()
    {
        return alive;
    }

    public static Player getInstance()
    {
        return instance;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // Update is called once per frame
	void Update () {
		
	}
}
