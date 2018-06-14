using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    private float lives = 1;
    private bool alive = false;
    public float health = 50;
    private Game game;
    private Character character;

    void start()
    {
        this.character = new TestCharacter() ;
        health = character.GetHealth();
        game = Game.GetInstance();
        lives = game.GetLives();
        //this.name = character.name;
    }

    public void TakeDamage(float damage)
    {
        float damageTake = character.CalculateDamage(damage);
        if(health - damageTake <= 0 )
        {
            notifyDeath();
        } else
        {
            health -= damageTake;
        }
    }
	
    public bool IsAlive()
    {
        return alive;
    }

    
    public void notifyDeath()
    {
        game.TriggerDeath(this);
    }
    
    // Update is called once per frame
	void Update () {
		
	}
}
