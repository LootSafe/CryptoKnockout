using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCountDown : MonoBehaviour {
    public GameObject particles;
    public Text display;
    public Outline o;
    private AudioSource audioSource;
    public AudioClip countdownClip;
    public AudioClip fightClip;

    private bool fightLock;

    private int lastTime;
    Game game;
	// Use this for initialization
	void Start ()
    {
        display.gameObject.SetActive(false);
        particles.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        AudioSystem.Register(audioSource);
    }
	
    void OnEnable()
    {
        fightLock = false;
        lastTime = (int)game.GetRemainingCountDownTime();
    }
	// Update is called once per frame
	void Update () {
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }

		if(game.GetState() == Game.State.ROUND_BEGINING)
        {

            if ((int)game.GetRemainingCountDownTime() < 1)
            {
                if (!fightLock)
                {
                    PlayAudio.Play(audioSource, fightClip);
                    fightLock = true;
                }
                
                display.fontSize = 300;
                display.text = "FIGHT";
                particles.SetActive(true);
                o.effectDistance.Set(10, 10);

            }
            else
            {
                display.gameObject.SetActive(true);
                display.text = (int)game.GetRemainingCountDownTime() + "";
                if(lastTime != (int)game.GetRemainingCountDownTime())
                {
                    PlayAudio.Play(audioSource, countdownClip);
                }
                lastTime = (int)game.GetRemainingCountDownTime();
                float f = 1 - (game.GetRemainingCountDownTime() - Mathf.FloorToInt(game.GetRemainingCountDownTime()));
                display.fontSize = (int)(f * 200);
            }
        } else
        {
            o.effectDistance = new Vector2(2, 2);
            display.gameObject.SetActive(false);
            particles.SetActive(false);
        }
	}
}
