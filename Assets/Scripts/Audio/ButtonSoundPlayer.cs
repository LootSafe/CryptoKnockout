using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip clickSound;

	// Use this for initialization
	void Start () {
        audioSource = transform.parent.GetComponent<AudioSource>();
        AudioSystem.Register(audioSource);
        if (!selectSound) selectSound = GlobalGameData.GetInstance().selectSound;
        if (!clickSound) clickSound = GlobalGameData.GetInstance().clickSound;
    }

    void OnSelect()
    {
        PlayAudio.Play(audioSource, selectSound);
    }

    void OnClick()
    {
        PlayAudio.Play(audioSource, clickSound);
    }
}
