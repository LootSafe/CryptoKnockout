using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthbotSuper : SuperAnimationControl {
    private int yDirection, xDirection;
    public float runSpeed = 0.4f;
    public GameObject opponentHit;

    public AudioSource audioSource;
    public AudioClip chargingAudio;
    public AudioClip explosionAudio;
    public AudioClip hahaAudio;

    public float explosionLength = 0.1f;
    private float explosionTime;

    public GameObject explosion;
    private bool hasExploded;

    public override void Start()
    {
        base.Start();
    }


    public override void StartSequence()
    {
        animationObject.SetActive(false);
        base.StartSequence();

        if (opponent.transform.position.y < transform.position.y)
        {
            yDirection = -1;
        }
        else
        {
            yDirection = 1;
        }

        if (opponent.transform.position.x < transform.position.x)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        AudioSystem.Play(audioSource, chargingAudio);
        hasExploded = false;
        base.StartSequence();
    }

    public override void UpdateIntro()
    {
        if (Mathf.Abs(transform.position.x - opponent.transform.position.x) > opponent.GetComponent<BoxCollider2D>().size.x / 3)
        {
            Debug.Log(runSpeed * xDirection);
            rigidbody.AddForce(new Vector2(runSpeed * xDirection, 0));
            player.SetOppositeHeading(opponent.GetHeading());
        }
        else
        {
            animationObject.SetActive(true);
            opponentHit.transform.position = opponent.transform.position + new Vector3(0, 0.6f, 0);
            AudioSystem.Play(audioSource, explosionAudio);
            rigidbody.velocity = Vector2.zero;
            NextSequence();
            explosionTime = Time.time + explosionLength;
        }
    }
    public override void UpdateMid()
    {
        if (!hasExploded && Time.time >= explosionTime)
        {
            hasExploded = true;
            explosion.SetActive(true);
        }

        if (Time.time >= postTime)
        {
            AudioSystem.Play(audioSource, explosionAudio);
            animationObject.SetActive(false);
            NextSequence();
        }
    }
    public override void UpdatePost()
    {
        if (Time.time >= endTime)
        {
            explosion.SetActive(false);
            opponentHit.SetActive(true);
            NextSequence();
        }
    }

    public override void UpdateEnd()
    {

        if (Time.time >= waitTime)
        {
            opponentHit.SetActive(false);
            player.NotifySuperComplete();
            AudioSystem.Play(audioSource, hahaAudio);
            NextSequence();
        }
    }
}
