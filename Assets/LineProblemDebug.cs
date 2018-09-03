using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineProblemDebug : MonoBehaviour {
    public GameObject testDot;
    private float lastCall;
	// Use this for initialization
	void Start () {
        lastCall = 5f;
        lastCall = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= lastCall + 0.5)
        {
            Instantiate(testDot,transform.position, transform.rotation);
            Debug.Log("Debug Dot Placed at:" + transform.position);
            lastCall = Time.time;
        }
        
    }
}
