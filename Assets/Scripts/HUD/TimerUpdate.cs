using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUpdate : MonoBehaviour {
    public Text display;
    Game game;
	// Use this for initialization
	void Start () {
        //display.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }

            int secondsLeft = (int)game.GetRemainingRoundTime();
            if(secondsLeft < 1)
            {
                secondsLeft = 0;
            }
            display.text = secondsLeft + "";
        
	}
}
