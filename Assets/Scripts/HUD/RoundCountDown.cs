using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCountDown : MonoBehaviour {
    public Text display;
    Game game;
	// Use this for initialization
	void Start () {
        display.gameObject.SetActive(false);
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
                //Temp
                display.text = "FIGHT";
            }
            else
            {
                display.gameObject.SetActive(true);
                display.text = (int)game.GetRemainingCountDownTime() + "";
            }
        } else
        {
            display.gameObject.SetActive(false);
        }
	}
}
