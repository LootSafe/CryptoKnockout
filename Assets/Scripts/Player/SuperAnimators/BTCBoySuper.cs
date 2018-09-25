using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCBoySuper : SuperAnimationControl {

    Transform target;
    int yDirection;
    int xDirection;
    float originalGravity;
    public override void Start()
    {
        base.Start();
        target = GameObject.Find("btcJumpTarget").transform;
    }

    public override void StartSequence()
    {
        originalGravity = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().gravityScale = 0;

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
        //Target Location
        //Animate Player Position to Go There ?
        Debug.Log("Current Position " + transform.position);

        transform.position = new Vector2(0 , yDirection) + (Vector2)transform.position;

        Debug.Log("New Position " + (new Vector2(xDirection, 0) + (Vector2)transform.position));
        if (Mathf.Abs(transform.position.y - target.position.y) <2)
        {
            transform.position = new Vector2(Game.GetInstance().GetOpponent(player.GetPlayerNumber()).transform.position.x, transform.position.y);
            GetComponent<Rigidbody2D>().gravityScale = originalGravity;
            NextSequence();
        }
    }

    public override void UpdateMid()
    {

        base.UpdateMid();
    }

}
