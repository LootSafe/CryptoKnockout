using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCBoySuper : SuperAnimationControl {

    Transform target;
    int yDirection;
    int xDirection;
    float originalGravity;
    public float animationSpeed = 1;

    public override void Start()
    {
        base.Start();
        target = GameObject.Find("btcJumpTarget").transform;
    }

    public override void StartSequence()
    {
        originalGravity = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().simulated = false;
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
            NextSequence();
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    public override void UpdatePost()
    {
        if (Time.time >= endTime)
        {
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
