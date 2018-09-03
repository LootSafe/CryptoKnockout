using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugObjRemover : MonoBehaviour {
    private float started;
    public float lifeTime = 8;
	// Use this for initialization
	void Start () {
        started = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= started + lifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
