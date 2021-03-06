﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerEntity : MonoBehaviour {
    private float lives = 1;
    private bool alive = false;
    public float health = 0;
    public Transform target;
    public float special = 0;
    private float maxSpecial = 100;
    private float maxHealth;
    private Game game;
    [SerializeField]
    public Character character;

    public GameObject fist;
    public GameObject foot;
    public GameObject specialSource;

    private bool hurt;
    public float timeIncapacitated = 0.2f;

    private float lastHit;
    private float damageDealt;
    public int currentStreak;
    private float lastDamageDealt;
    private float lastDamageDealtTime;

    private int score;

    public bool attacking = false;
    private bool blocking , ducking;
    private bool grounded = false;

    private int playerNumber;
    private AudioSource audioSource;
    public AudioClip damageBlockSound;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public Sprite characterPortrait;

    public SuperAnimationControl superAnimationControl;
    private bool superFinished;
    private ActionLocks locks;

    void Start()
    {
        game = Game.GetInstance();
        lives = game.GetLives();
        game.RegisterPlayer(this, GetComponent<NetworkIdentity>());
        audioSource = GetComponent<AudioSource>();
        AudioSystem.Register(audioSource);
        locks = new ActionLocks(this);
    }

    public void InitializeWithCharacter(Character character)
    {

        //Animation Controller
        //GetComponent<Animator>().runtimeAnimatorController = character.GetAnimationController();
        //Debug.Log("Test");
        character.initializePlayer(this);
        this.character = character;
        health = character.GetHealth();
        maxSpecial = character.GetSpecial();
        maxHealth = character.GetHealth();
        this.name = character.GetName();
        special = 1;
    }

    public void OnDestroy()
    {
        if (!game) return;
        game.UnregisterPlayer(this, GetComponent<NetworkIdentity>());
    }

    //Check For Grounded
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {

            if (!grounded)
            {
                GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.LAND);
                //Debug.Log("LANDED");
            }
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            grounded = false;
        }
    }


            //To be done only by server
    public float TakeDamage(float damage, PlayerEntity source)
    {

        if (!alive) return 0;
        //Temp
        float damageTake = character.CalculateDamageTaken(damage);

        if (game.GetGameMode() == Game.GameMode.LOCALMULTIPLAYER)
        {
            
            if (health - damageTake <= 0)
            {
                health = 0;
                lastHit = Time.time;
                notifyDeath();
                GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.DEAD);
                GetComponent<DamageAnimator>().TriggerSmallHit(damageTake, source, damageTake > maxHealth * .09);
            }
            else
            {
                if (damageTake <= 0) {
                    PlayAudio.Play(audioSource, damageBlockSound);
                    return 0;

                }
                currentStreak = 0;
                health -= damageTake;
                lastHit = Time.time;
                lastDamageDealt = damageDealt;
                hurt = true;
                InterruptActions();
                GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HURT);
                GetComponent<DamageAnimator>().TriggerSmallHit(damageTake, source, damageTake > maxHealth * .09);
                if (source)
                {
                    KnockBack(source.GetComponent<Transform>().position.x);
                }
            }
        }
        
        
        
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
        if(damageDealt > 0 )
        {
            if (Time.time < lastDamageDealtTime + game.streakTime || currentStreak == 0)
            {
                AddSuccessfulHit(damageDealt);

            }
        } else
        {
            lastDamageDealt = 0;
        }

        special += (5+ (damageDealt / 2) + (damageDealt * currentStreak)) / 2;
        this.damageDealt += damageDealt;
    }

    public void InterruptActions()
    {
        fist.SetActive(false);
        foot.SetActive(false);
        attacking = false;
    }
	
    public bool IsAlive()
    {
        return alive;
    }

    public bool IsHurt()
    {
        return hurt;
    }

    public void StartAttacking()
    {
        attacking = true;
    }

    public void StopAttacking()
    {
        attacking = false;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public void StartBlocking()
    {
        blocking = true;
    }

    public void StopBlocking()
    {
        blocking = false;
    }

    public bool IsDucking()
    {
        return ducking;
    }

    public void StartDucking()
    {
        ducking = true;
    }

    public void StopDucking()
    {
        ducking = false;
    }
    public bool IsBlocking()
    {
        return blocking;
    }
    public bool IsGrounded()
    {
        return grounded;
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

    public int GetStreak()
    {
        return currentStreak;
    }
    public void AddSuccessfulHit(float damageDealt)
    {
        lastDamageDealtTime = Time.time;
        lastDamageDealt = damageDealt;
        currentStreak += 1;
    }

    public float GetLastDamageDealt()
    {
        return lastDamageDealt;
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

    public List<string> GetHitWords()
    {
        return character.GetHitWords();
    }

    public string RequestHitWord()
    {
        return GetHitWords()[Random.Range((int)0, GetHitWords().Count)];
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public bool AnyLock()
    {
        return locks.AnyLock();
    }

    public ActionLocks Locks()
    {
        return locks;
    }

    public float GetDamageDealt()
    {
        return damageDealt;
    }

    public void SetPlayerNumber(int n)
    {
        playerNumber = n;
    }

    public int GetScore()
    {
        return score;
    }

    public void notifyDeath()
    {
        lives--;
        alive = false;
        game.TriggerDeath(this);
    }
    
    public void AddToScore()
    {
        score += 1;
    }

    public bool UseSuper()
    {
        if (special >= maxSpecial)
        {
            if (game.TriggerSuper(this))
            {
                special = 0;
                superAnimationControl.StartSequence();
                return true;
            }
        }
        return false;
    }

    public bool IsSuperFinished()
    {
        if (superFinished)
        {
            superFinished = false;
            return true;
        }
        return false;
    }

    public void NotifySuperComplete()
    {
        superFinished = true;
    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = audioClip;
        audioSource.time = 0;
        audioSource.Play();
    }

    public void respawn()
    {
        alive = true;
        attacking = false;
        blocking = false;
        ducking = false;
        health = character.GetHealth();
        lastDamageDealt = 0;
        currentStreak = 0;
        //GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.ALIVE);
        special = 0;

    }
    // Update is called once per frame
	void Update () {
		
        //Update Hurt Interruption
        if(Time.time >= lastHit + timeIncapacitated)
        {
            hurt = false;
        }

        //Udate Streak Time
        if(Time.time >= lastDamageDealtTime + game.streakTime)
        {
            currentStreak = 0;
        }
	}

    public void SetHeading(Headings heading)
    {
        Transform transform = GetComponentInParent<Transform>();
        switch (heading)
        {
            case Headings.LEFT:
                transform.localScale = new Vector3(-1 * (Mathf.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
                break;
            case Headings.RIGHT:
                GetComponentInParent<Transform>().localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }

    public void SetOppositeHeading(Headings heading)
    {
        Transform transform = GetComponentInParent<Transform>();
        switch (heading)
        {
            case Headings.LEFT:
                transform.localScale = new Vector3((Mathf.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
                break;
            case Headings.RIGHT:
                GetComponentInParent<Transform>().localScale = new Vector3( -1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }

    public Headings GetHeading()
    {
        if(GetComponentInParent<Transform>().localScale.x > 0)
        {
            Debug.Log("Player is facing Right");
            return Headings.RIGHT;
        }
        else
        {
            return Headings.LEFT;
        }
    }


   
}
