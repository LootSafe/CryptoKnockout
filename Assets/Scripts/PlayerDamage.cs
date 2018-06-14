using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && other!= player)
        {
            other.GetComponent<Player>().TakeDamage(10);
        }
    }
}
