using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundVictoryUpdater : MonoBehaviour {

    public AudioClip KOSound;

    Game game;
    public Text text;
    bool audioLock = false;
	// Use this for initialization
	void Start () {
        AudioSystem.Register(GetComponent<AudioSource>());
	}
	
	// Update is called once per frame
	void Update () {
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }

        if(game.GetState() == Game.State.ROUND_ENDING)
        {
            if (!audioLock)
            {
                PlayAudio.Play(GetComponent<AudioSource>(), KOSound);
                audioLock = true;
            }
            text.gameObject.SetActive(true);
            text.text = "PLAYER " + game.GetRoundWinner() + " WINS";
        }
        else
        {
            text.gameObject.SetActive(false);
            audioLock = false;
        }
	}
}
