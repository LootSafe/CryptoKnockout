using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundVictoryUpdater : MonoBehaviour {


    Game game;
    public Text text;
	// Use this for initialization
	void Start () {
		
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
            text = "PLAYER # WINS";
        }

	}
}
