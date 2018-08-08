using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScroller : MonoBehaviour {

    public float speed = 0.1f;
    public float direction = -1;
    public Transform leftEdge;
    public Transform rightEdge;
    public Transform[] objects;

    private int leading;
    private Transform following;
    private Vector2 orgFollowingPosition;

    

	// Use this for initialization
	void Start () {
        leading = 0;
        following = objects[objects.Length - 1];
        orgFollowingPosition = following.position;
	}
	
	// Update is called once per frame
	void Update () {

        //Reset leading 
        foreach (Transform obj in objects)
        {
            obj.position = new Vector2(obj.position.x + (speed * direction), obj.position.y);
        }
        if (objects[leading].position.x < leftEdge.position.x - objects[leading].GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            objects[leading].position = orgFollowingPosition;
            leading = nextObject();
        }

    }

    int nextObject()
    {
        if(leading >= objects.Length - 1)
        {
            return 0;
        } else
        {
            return ++leading;
        }
    }

}
