using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if(other.tag == "Player" && other!= player)
        {
            other.GetComponent<Player>().TakeDamage(10);
        }
    }
}
