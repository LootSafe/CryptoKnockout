using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneroSuper : SuperAnimationControl {

    public GameObject portalObject;
    private Vector2 portalLocation;

    public AudioSource audioSource;
    public AudioClip witchLaugh;
    public AudioClip portalSound;
    public AudioClip firing;

    private Vector2 originalLocation;

    private float portalWaitTime;
    public float portalWaitLength;
    public bool portalSet;

    private int xDirection, yDirection;
    public GameObject hitObject;

    public override void Start()
    {
        
        base.Start();
    }

    public override void StartSequence()
    {
        originalLocation = transform.position;
        AudioSystem.Play(audioSource, portalSound);

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
        base.StartSequence();
        portalSet = false;
        portalLocation = new Vector2(opponent.transform.position.x - (1 * xDirection), portalObject.transform.position.y);
        portalWaitTime = Time.time + portalWaitLength;
        portalObject.transform.position = new Vector2(transform.position.x, portalObject.transform.position.y);
        portalObject.SetActive(true);
    }


    public override void UpdateIntro()
    {
        if(Time.time >= portalWaitTime && !portalSet)
        {
            portalSet = true;
            //PortalObject.transform.position = portalLocation;
            transform.position = new Vector2(portalLocation.x, transform.position.y);
        }
        if(Time.time >= midTime)
        {
            AudioSystem.Play(audioSource, firing);
            NextSequence();
        }
    }
    public override void UpdateMid()
    {
        if(Time.time >= postTime)
        {
            
            
            portalObject.SetActive(false);
            NextSequence();
        }
    }
    public override void UpdatePost()
    {
        if(Time.time >= endTime)
        {
            transform.position = originalLocation;
            NextSequence();
            player.NotifySuperComplete();
        }
    }

    public override void UpdateEnd()
    {
        base.UpdateEnd();
    }
}
