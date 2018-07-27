using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseGallop : MonoBehaviour {

    GameObject[] markers;
    Vector2 currentTarget;
    bool arrived = false;
    public float arrivedDistance = 2;
    public float speed = 0.2f;
	// Use this for initialization
	void Start () {
        markers = GameObject.FindGameObjectsWithTag("BackgroundMarkers");
        currentTarget = new Vector2(markers[0].transform.position.x, markers[0].transform.position.y);

    }
	
	// Update is called once per frame
	void Update () {
        //If arrived
        if (!arrived && Mathf.Abs(transform.position.x - currentTarget.x) <= arrivedDistance)
        {
            arrived = true;
        }
        // Get New Target Location
        // Location Markers --> 5
        if (currentTarget == null || arrived)
        {
            float targetX = markers[Random.Range(0, markers.Length)].transform.position.x;
            float targetY = markers[Random.Range(0, markers.Length)].transform.position.y;
            currentTarget = new Vector2(targetX, targetY);
            arrived = false;
        } else
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }




	}
}
