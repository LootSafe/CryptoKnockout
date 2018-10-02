using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdatePortrait : MonoBehaviour {


    public HealthBarUpdater hbu;
    private Game game;
    PlayerEntity player;
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


	}
}
