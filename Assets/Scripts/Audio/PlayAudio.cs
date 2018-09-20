using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio {

    public static void Play(AudioSource audioSource, AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = 0;
        audioSource.loop = false;
        audioSource.Play();
    }
}
