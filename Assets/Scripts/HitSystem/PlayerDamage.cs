using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode()]
public class PlayerDamage : MonoBehaviour {
    public Player player;
    float lastHit;
    public float delay = .02f;
    public float overrideDamage = 10;
    private bool state;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    void Update()
    {

        if (player)
        {
            var p = transform.localPosition;
            if (state)
            {
                p.x += 1;
            }
            else
            {
                p.x -= 1;
            }
        }
        if (lastHit != 0 && Time.time >= lastHit + delay)
        {
            gameObject.SetActive(false);
            if (player) player.StopAttacking();
            lastHit = 0;
        }

    }

    public void TriggerEnable()
    {
        overrideDamage = 10;
        lastHit = Time.time;
        gameObject.SetActive(true);
    }

    public void TriggerEnable(float overrideDamage)
    {
        this.overrideDamage = overrideDamage;
        lastHit = Time.time;
        gameObject.SetActive(true);
    }

    public void TriggerDisable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && other != player)
        {
            float damageDealt = other.GetComponent<Player>().TakeDamage(overrideDamage, player);
            gameObject.SetActive(false);
            if (player)
            {
                player.AddToScore(damageDealt);
                player.StopAttacking();
            }
        }
    }
}
