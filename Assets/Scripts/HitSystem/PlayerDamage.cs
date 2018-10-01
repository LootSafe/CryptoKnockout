using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    public PlayerEntity player;
    float lastHit;
    public float delay = .02f;
    public float overrideDamage = 10;
    private bool state;
    public AudioSource audioSource;
    public AudioClip damageSound;
    //public AudioClip damageBlockedSound;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerEntity>();
        AudioSystem.Register(audioSource);
    }

    void OnEnable()
    {
        if (audioSource)
        {
            audioSource.Stop();
        }
    }
    void Update()
    {

        if (player)
        {
            var p = transform.localPosition;
            if (state)
            {
                p.x += 3;
            }
            else
            {
                p.x -= 3;
            }
        }
        if (lastHit != 0 && Time.time >= lastHit + delay)
        {
            gameObject.SetActive(false);
            if (player) player.StopAttacking();
            lastHit = 0;
        }

    }

    public void TriggerEnable()
    {
        overrideDamage = 10;
        lastHit = Time.time;
        gameObject.SetActive(true);
    }

    public void TriggerEnable(float overrideDamage)
    {
        this.overrideDamage = overrideDamage;
        lastHit = Time.time;
        gameObject.SetActive(true);
    }

    public void TriggerDisable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject == player.gameObject) return;

            float damageDealt = other.GetComponent<PlayerEntity>().TakeDamage(overrideDamage, player);
            gameObject.SetActive(false);
            if (player)
            {
                //Debug.Log("Damage Dealt  " + damageDealt);
                if (damageDealt > 0)
                {
                    PlayAudio.Play(audioSource, damageSound);
                }
            
                player.AddToScore(damageDealt);
                player.StopAttacking();
            }
        }
    }
}
