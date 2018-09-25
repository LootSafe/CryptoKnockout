using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] tracks;
    private float nextSongTime;
    

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (tracks.Length > 0)
        {
            if (Time.time >= nextSongTime)
            {
                AudioClip track = tracks[Random.Range((int)0, (int)tracks.Length)];
                PlayAudio.Play(audioSource, track);
                nextSongTime = Time.time + track.length;
            }
        }
	}
}
