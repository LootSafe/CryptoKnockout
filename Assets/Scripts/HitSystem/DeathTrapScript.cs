using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrapScript : MonoBehaviour {

    public PlayerEntity player;
    public float damage = 10f;
    float lastHit;
    public float delay = .02f;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerEntity>();
    }

    void Update()
    {
    }

    public void TriggerEnable()
    {
        lastHit = Time.time;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && other != player)
        {
            float damageDealt = other.GetComponent<PlayerEntity>().TakeDamage(damage, player);
            if (player) player.AddToScore(damageDealt);
        }
    }
}
