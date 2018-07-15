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
        string displayString = "";

            int secondsLeft = (int)game.GetRemainingRoundTime();
            if(secondsLeft < 1)
            {
                secondsLeft = 0;
            }
        int minutes = secondsLeft / 60;
        int seconds = secondsLeft % 60;

        displayString = minutes + ":";
        if(seconds < 10)
        {
            displayString = displayString + "0" + seconds;
        } else
        {
            displayString = displayString + seconds;
        }

        display.text = displayString;
        

        
	}
}
