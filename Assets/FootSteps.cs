using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour {

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        AudioSystem.Register(audioSource);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
