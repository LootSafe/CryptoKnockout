using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCBoySuper : SuperAnimationControl {

    Transform target;
    int yDirection;
    int xDirection;
    float originalGravity;
    public float animationSpeed = 1;

    private AudioSource audioSource;
    public AudioClip soundRising;
    public AudioClip soundDing;
    public AudioClip soundFalling;
    public AudioClip soundEnd;

    public override void Start()
    {
        base.Start();
        target = GameObject.Find("btcJumpTarget").transform;
        audioSource = AnimationObject.GetComponent<AudioSource>();
    }

    public override void StartSequence()
    {
        PlayAudio.Play(audioSource, soundRising);
        originalGravity = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        AnimationObject.SetActive(true);

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
            Debug.Log("trying this thing");

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
        if (Time.time >= endTime)
        {
            PlayAudio.Play(audioSource, soundEnd);
            NextSequence();
            player.NotifySuperComplete();
        }
    }

    public override void UpdateEnd()
    {
        if (Time.time >= waitTime)
        {
            AnimationObject.SetActive(false);
            NextSequence();
        }
    }

}
