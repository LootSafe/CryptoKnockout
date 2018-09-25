using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeSuper : SuperAnimationControl {

    Transform target;
    AudioSource audioSource;

    public AudioClip laserWarmUp;
    public AudioClip laserFire;
    public AudioClip chainFall;

    private int xDirection, yDirection;
    public override void Start()
    {
        audioSource = animationObject.GetComponent<AudioSource>();
        target = Game.GetInstance().GetOpponent(player.GetPlayerNumber()).transform;
        base.Start();
    }

    public override void StartSequence()
    {
        PlayAudio.Play(audioSource, laserWarmUp);
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);


        if (target.position.y < transform.position.y)
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
        if (Time.time >= midTime)
        {
            AudioSystem.Play(audioSource, laserFire, true);
            animationObject.SetActive(true);
            NextSequence();
        }
    }

    public override void UpdateMid()
    {
        if (Time.time >= postTime)
        {
            AudioSystem.Play(audioSource, chainFall);
            NextSequence();
        }
    }

    public override void UpdatePost()
    {
        if (Time.time >= endTime)
        {
            animationObject.SetActive(false);
            NextSequence();
        }
    }

    public override void UpdateEnd()
    {
        base.UpdateEnd();
    }


}
