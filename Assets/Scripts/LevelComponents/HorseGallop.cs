using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseGallop : MonoBehaviour {

    GameObject[] markers;
    public Vector2 currentTarget;
    bool arrived = false;
    public float arrivedDistance = 2;
    public float speed = 0.2f;
    public Transform sprite;
    public float pauseFrequency = 0.2f;
    public float pauseTimeMin = 0.1f;
    public float pauseTimeMax = 2f;
    public float pauseWaitMin = 2f;
    public float pauseWaitMax = 5f;

    private bool paused;

    private float pauseTimer;

	// Use this for initialization
	void Start () {
        markers = GameObject.FindGameObjectsWithTag("BackgroundMarkers");
        currentTarget = new Vector2(markers[0].transform.position.x, markers[0].transform.position.y);

    }
	
	// Update is called once per frame
	void Update () {
        UpdatePauseSequence();
        UpdatePosition();
	}

    void UpdatePauseSequence()
    {
        if (paused)
        {
            if(Time.time >= pauseTimer)
            {
                paused = false;
                if (Random.Range(0, 1) > 0.5f)
                {
                    CycleTarget();
                }
            }
            
        } else
        {

            if (Time.time >= pauseTimer + Random.Range(pauseWaitMin, pauseWaitMax))
            {
                if(Random.Range(0,2) <= pauseFrequency)
                {
                    Pause();
                }
            }
        }
    }

    void UpdatePosition()
    {
        if (paused) return;

        int direction = 0;
        if (currentTarget.x > transform.position.x)
        {
            direction = 1;
                sprite.localScale = new Vector3(-Mathf.Abs(sprite.localScale.x), sprite.localScale.y, sprite.localScale.z);
            
        }
        else
        {
            direction = -1;
            sprite.localScale = new Vector3(Mathf.Abs(sprite.localScale.x), sprite.localScale.y, sprite.localScale.z);
        }
        //If arrived
        if (!arrived && Mathf.Abs(transform.position.x - currentTarget.x) <= arrivedDistance)
        {
            arrived = true;
        }
        // Get New Target Location
        // Location Markers --> 5
        if (currentTarget == null || arrived)
        {
            CycleTarget();
            arrived = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x + (speed * direction), transform.position.y);
        }
    }

    public void Pause()
    {
        paused = true;
        pauseTimer = Time.time + Random.Range(pauseTimeMin, pauseTimeMax);
    }

    public void Resume()
    {
        paused = false;
        pauseTimer = 0f;
    }

    public void CycleTarget()
    {
        float targetX = markers[Random.Range(0, markers.Length)].transform.position.x;
        float targetY = markers[Random.Range(0, markers.Length)].transform.position.y;
        currentTarget = new Vector2(targetX, targetY);
    }
}
