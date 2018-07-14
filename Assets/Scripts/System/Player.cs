using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode()]
public class Player : NetworkBehaviour {
    [SyncVar]
    private float lives = 1;
    [SyncVar]
    private bool alive = false;
    [SyncVar]
    public float health = 0;
    private float special = 0;
    private float maxSpecial = 100;
    private float maxHealth;
    private Game game;
    private Character character;
    public GameObject fist;
    public GameObject foot;
    public GameObject specialSource;

    private float lastHit;

    void Start()
    {
        game = Game.GetInstance();
        lives = game.GetLives();
        game.RegisterPlayer(this, GetComponent<NetworkIdentity>());
    }

    public void InitializeWithCharacter(Character character)
    {

        //Animation Controller
        GetComponent<Animator>().runtimeAnimatorController = character.GetAnimationController();
        Debug.Log("Test");
        character.initializePlayer(this);
        this.character = character;
        health = character.GetHealth();
        maxSpecial = character.GetSpecial();
        maxHealth = character.GetHealth();
        this.name = character.GetName();        
    }

    public void OnDestroy()
    {
        if (!game) return;
        game.UnregisterPlayer(this, GetComponent<NetworkIdentity>());
    }

    //To be done only by server
    public void TakeDamage(float damage)
    {
        if (!isServer) return;
        
        Debug.Log("Taking Damage");
        float damageTake = character.CalculateDamage(damage);
        if(health - damageTake <= 0 )
        {
            health = 0;
            notifyDeath();
        } else
        {
            health -= damageTake;
            lastHit = Time.time;
        }
    }
	
    public bool IsAlive()
    {
        return alive;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetSpecial()
    {
        return special;
    }

    public float GetMaxSpecial()
    {
        return maxSpecial;
    }


    public float GetLastHit()
    {
        return lastHit;
    }
    public float GetMoveSpeed()
    {
       return character.GetMoveSpeed();
    }
    public Character GetCharacter()
    {
        return character;
    }
    public void notifyDeath()
    {
        game.TriggerDeath(this);
        respawn();
    }
    
    public void respawn()
    {
        lives--;
        health = character.GetHealth();
        special = 0;

    }
    // Update is called once per frame
	void Update () {
		
	}
}
