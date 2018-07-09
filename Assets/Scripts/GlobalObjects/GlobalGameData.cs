using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameData : MonoBehaviour {

    public Game.GameMode selectedGameMode = Game.GameMode.LOCALMULTIPLAYER;
    public string selectedLevel = "Denver";
    public string player1Char = "Bjorn";
    public string player2Char = "Doge";


    private static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

}
