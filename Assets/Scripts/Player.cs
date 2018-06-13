using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxHealth;
   
    private float lives;
    private bool alive;
    private float health;
    private Game game;

    public Player(Character character)
    {
        health = character.GetHealth();
        game = Game.GetInstance();
        lives = game.GetLives();
    }


    public void TakeDamage(float damage)
    {

    }
	
    public bool IsAlive()
    {
        return alive;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // Update is called once per frame
	void Update () {
		
	}
}
