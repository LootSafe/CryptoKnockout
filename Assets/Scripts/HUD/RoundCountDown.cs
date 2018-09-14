using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCountDown : MonoBehaviour {
    public GameObject particles;
    public Text display;
    public Outline o;
    Game game;
	// Use this for initialization
	void Start ()
    {
        display.gameObject.SetActive(false);
        particles.SetActive(false);
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
                display.fontSize = 300;
                display.text = "FIGHT";
                particles.SetActive(true);
                o.effectDistance.Set(10, 10);
            }
            else
            {
                display.gameObject.SetActive(true);
                display.text = (int)game.GetRemainingCountDownTime() + "";

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
