using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRegistrar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSystem.Register(GetComponent<AudioSource>());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
