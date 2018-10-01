using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameData : MonoBehaviour {

    public Game.GameMode selectedGameMode = Game.GameMode.LOCALMULTIPLAYER;
    public string selectedLevel = "Denver";
    public Characters player1Char = Characters.BITCOINBOY;
    public Characters player2Char = Characters.ETHBOT;

    public float volume = 0.007f;

    public AudioClip clickSound;
    public AudioClip selectSound;
    public AudioClip errorSound;

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
