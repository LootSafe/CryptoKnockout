using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRandomScript : MonoBehaviour {

    public GameObject laserSprite;
    public float delay = 1f;
    public float fireTime =  1f;
    private float lastFire;
    private bool active;
    Game game;
	// Use this for initialization
	void Start () {
        laserSprite.SetActive(false);
        lastFire = Time.time;
        game = Game.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }
        if (active)
        {
            if (Time.time - lastFire >= fireTime + Random.Range(0.1f, 0.3f))
            {
                lastFire = Time.time;
                laserSprite.SetActive(false);
                active = false;
            }
        }
        else
        {
            if (game.GetState() != Game.State.FIGHTING) return;
            if (Time.time - lastFire >= delay + Random.Range(0.1f, 10f))
            {
                
                lastFire = Time.time;
                laserSprite.SetActive(true);
                active = true;
            }
        }
	}
}
