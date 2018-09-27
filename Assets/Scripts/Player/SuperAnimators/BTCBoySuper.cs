using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCBoySuper : SuperAnimationControl {

    Transform target;
    int yDirection;
    int xDirection;

    public float animationSpeed = 1;
    
    private AudioSource audioSource;
    public AudioClip soundRising;
    public AudioClip soundDing;
    public AudioClip soundFalling;
    public AudioClip soundEnd;

    public BTCExplosionManager explosion;
    bool explosionTrigger;

    public override void Start()
    {
        base.Start();
        target = GameObject.Find("btcJumpTarget").transform;
        audioSource = animationObject.GetComponent<AudioSource>();
    }

    public override void StartSequence()
    {
        animationObject.SetActive(true);
        PlayAudio.Play(audioSource, soundRising);
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        explosion.Reset();

        if(target.position.y < transform.position.y)
        {
            yDirection = -1;
        }
        else
        {
            yDirection = 1;
        }

        if (target.position.x < transform.position.x)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        base.StartSequence();
    }

    public override void UpdateIntro()
    {

        if (Mathf.Abs(transform.position.y - target.position.y) > 2)
        {
            transform.position = new Vector2(0, yDirection * animationSpeed) + (Vector2)transform.position;
            //Debug.Log("trying this thing");

        }
        else
        {
            if (Time.time >= midTime)
            {
                PlayAudio.Play(audioSource, soundDing);
                Debug.Log("It's Time");
                transform.position = new Vector2(Game.GetInstance().GetOpponent(player.GetPlayerNumber()).transform.position.x, transform.position.y);
                NextSequence();
            }
;
        }

    }

    public override void UpdateMid()
    {
        if (Time.time >= postTime)
        {
            PlayAudio.Play(audioSource, soundFalling);
            NextSequence();
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    public override void UpdatePost()
    {
        if (explosion.HasLanded())
        {
            explosion.TriggerExplosion();
            Debug.Log("Alright BTC BOy Landed");
            PlayAudio.Play(audioSource, soundEnd);
            waitTime = Time.time + waitLength;
            player.NotifySuperComplete();
            NextSequence();
        }
    }

    public override void UpdateEnd()
    {
        if (Time.time >= waitTime)
        {
            explosion.Reset();
            animationObject.SetActive(false);
            explosionTrigger = false;
            NextSequence();
        }
    }

}
