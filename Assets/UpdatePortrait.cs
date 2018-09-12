using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdatePortrait : MonoBehaviour {

    private Image image;
    public HealthBarUpdater hbu;
    private Game game;
    Player player;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();

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
