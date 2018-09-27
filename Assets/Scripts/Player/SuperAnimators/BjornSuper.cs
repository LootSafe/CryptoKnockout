using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BjornSuper : SuperAnimationControl{


    private int yDirection, xDirection;
    public float runSpeed = 0.4f;
    public GameObject opponentHit;

    public AudioSource audioSource;
    public AudioClip bladeAudio;
    public AudioClip energyAudio;
    public AudioClip RoarAudio;

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
        AudioSystem.Play(audioSource, RoarAudio);
        base.StartSequence();
    }

    public override void UpdateIntro()
    {
        if(Mathf.Abs(transform.position.x - opponent.transform.position.x) > opponent.GetComponent<BoxCollider2D>().size.x /3)
        {
            Debug.Log(runSpeed * xDirection);
            rigidbody.AddForce(new Vector2(runSpeed * xDirection, 0));
        }
        else
        {
            AudioSystem.Play(audioSource, bladeAudio);
            rigidbody.velocity = Vector2.zero;
            NextSequence();
        }
    }
    public override void UpdateMid()
    {
        
        if(Time.time >= postTime)
        {
            AudioSystem.Play(audioSource, energyAudio, true);
            opponentHit.transform.position = opponent.transform.position + new Vector3(0,0.6f,0);
            animationObject.SetActive(true);
            NextSequence();
        }
    }
    public override void UpdatePost()
    {
        if (Time.time >= endTime)
        {
            animationObject.SetActive(false);
            player.NotifySuperComplete();
            NextSequence();
        }
    }

    public override void UpdateEnd()
    {
        AudioSystem.Play(audioSource, RoarAudio);
        base.UpdateEnd();
    }
}
