using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRandomScript : MonoBehaviour {

    public GameObject laserRayObject;
    public GameObject laserLine;
    public float delay = 1f;
    public float fireTime =  1f;
    public float targetRange = 20f;
    public float sweepSpeed = 0.1f;
    public Transform LaserSource;
    private float lastFire;
    private bool active;
    private Player[] players;
    public Transform currentTarget;

    public int fireChance;
    public int fireChances;
    public Vector2 minOffset, maxOffset;
    private Vector2 lastTargetLocation;

    public Animator horseAnimator;
    public HorseGallop gallop;

    Game game;
	// Use this for initialization
	void Start () {
        laserRayObject.SetActive(false);
        laserLine.SetActive(false);
        lastFire = Time.time;
        game = Game.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        //Check for game started
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }

        //Get Players From Game
        UpdatePlayers();

        //Fire
        TriggerRandomFire();

	}

    void CheckForTargets()
    {
        List<Transform> viableTargets = new List<Transform>();
        //Check for Radius
        foreach (Player p in players)
        {
            if (Vector2.Distance(LaserSource.position, p.transform.position) <= targetRange)
            {
                viableTargets.Add(p.transform);
            }
        }
        if (viableTargets.Count >= 0)
        {
            currentTarget = viableTargets[Random.Range((int)0, (int)viableTargets.Count)];
        } else
        {
            currentTarget = null;
        }

    }

    void TriggerRandomFire()
    {

        if (active)
        {
            if (Time.time - lastFire >= fireTime + Random.Range(0.1f, 0.2f))
            {
                //Enable Motion
                horseAnimator.enabled = true;
                gallop.Resume();

                lastFire = Time.time;
                laserRayObject.SetActive(false);
                laserLine.SetActive(false);
                active = false;
            }
        }
        else
        {
            if (game.GetState() != Game.State.FIGHTING) return;
            CheckForTargets();
            if (currentTarget)
            {
                if (Time.time - lastFire >= delay + Random.Range(1f, 10f))
                {
                    int roll = Random.Range(0, fireChances);
                    if (roll > fireChance)
                    {
                        //Choose Offset For Arch
                        lastTargetLocation = (Vector2)currentTarget.position + new Vector2(Random.Range(minOffset.x, maxOffset.x), Random.Range(minOffset.y, maxOffset.y));
                        //Account for Sprite Padding
                        lastTargetLocation += new Vector2(currentTarget.GetComponent<SpriteRenderer>().size.x / 2, currentTarget.GetComponent<SpriteRenderer>().size.y / 2);

                        //Disable Motion
                        horseAnimator.enabled = false;
                        gallop.Pause();


                        lastFire = Time.time;
                        laserRayObject.SetActive(true);
                        laserLine.SetActive(true);
                        active = true;
                    }
                }
            }
        }
    }

    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }

    //TODO needs to be optimized
    void UpdatePlayers()
    {
        if(players == null || players.Length == 0)
        {
            players = game.GetPlayers();
            return;
        }
       foreach(Player p in players)
        {
            if (!p)
            {
                players = game.GetPlayers();
                return;
            }
        }
    }

    public Vector2 GetNextPoint()
    {
        float y = Mathf.Sin(lastTargetLocation.x);
        Vector2 nextPoint = new Vector2(lastTargetLocation.x, y + lastTargetLocation.y);
        lastTargetLocation += new Vector2(sweepSpeed, 0);
        //Debug.Log("Last Target Location" + lastTargetLocation);
        //return lastTargetLocation;
        //return currentTarget.position;
        return nextPoint;
    }
}
