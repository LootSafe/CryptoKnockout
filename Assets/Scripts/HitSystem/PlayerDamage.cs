﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode()]
public class PlayerDamage : MonoBehaviour {
    public Player player;
    float lastHit;
    public float delay = .02f;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    void Update()
    {
        if (lastHit != 0 && Time.time >= Time.time + delay)
        {
            gameObject.SetActive(false);
            lastHit = 0;
        }
    }

    public void TriggerEnable()
    {
        lastHit = Time.time;
        gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && other!= player)
        {
            float damageDealt = other.GetComponent<Player>().TakeDamage(10, player);
            gameObject.SetActive(false);
            if(player) player.AddToScore(damageDealt);
        }
    }
}