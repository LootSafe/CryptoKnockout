using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCCharacterCollider : MonoBehaviour {
    private bool landed;
    public PlayerEntity player;
    private Vector3 explosionLocation;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
    }

    public void Reset()
    {
        landed = false;
    }

    public bool HasLanded()
    {
        return landed;
    }

    public Vector3 GetLocation()
    {
        return explosionLocation;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Colliding");
        if (landed) return;
        if (other.tag == "Player")
        {
            if (other.gameObject == player.gameObject) return;
            explosionLocation = other.transform.position;
            landed = true;
        }
    }
}
