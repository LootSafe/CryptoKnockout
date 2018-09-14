using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameData : MonoBehaviour {

    public Game.GameMode selectedGameMode = Game.GameMode.LOCALMULTIPLAYER;
    public string selectedLevel = "Denver";
    public Character.Characters player1Char = Character.Characters.BJORN;
    public Character.Characters player2Char = Character.Characters.DOGE;

    public float volume;


    private static bool created = false;
    private static GlobalGameData instance;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    public static GlobalGameData GetInstance()
    {
        return instance;
    }

    public void SetVolume(float f)
    {
        volume = f;
    }
}
