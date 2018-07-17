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
    private float damageDealt;

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
    public float TakeDamage(float damage, Player source)
    {

        if (!alive) return 0;
        //Temp
        float damageTake = character.CalculateDamage(damage);

        if (game.GetGameMode() == Game.GameMode.LOCALMULTIPLAYER || isServer)
        {
            
            if (health - damageTake <= 0)
            {
                health = 0;
                lastHit = Time.time;
                notifyDeath();
                GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.DEAD);
            }
            else
            {
                Debug.Log("Ouch!");
                health -= damageTake;
                lastHit = Time.time;
                GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HURT);
                KnockBack(source.GetComponent<Transform>().position.x);
            }
        }

        GetComponent<DamageAnimator>().TriggerSmallHit(damageTake);
        return damageTake;

    }

    void KnockBack(float sourcePositionX)
    {
        if(sourcePositionX < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 100));
        } else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 100));
        }
    }

    public void AddToScore(float damageDealt)
    {
        special += 1;
        this.damageDealt += damageDealt;
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
        lives--;
        alive = false;
        game.TriggerDeath(this);
    }
    
    public void respawn()
    {
        alive = true;
        health = character.GetHealth();
        GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.IDLE);
        special = 0;

    }
    // Update is called once per frame
	void Update () {
		
	}
}
