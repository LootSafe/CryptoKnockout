using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoHealthUpdate : MonoBehaviour {
    private Player player;
	// Use this for initialization
	void Start () {
        player = Game.GetInstance().GetPlayer();
        Debug.Log(player.name);
	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            player = Game.GetInstance().GetPlayer();
            return;
        }
        GetComponent<Text>().text = "" + player.GetHealth();
	}
}
