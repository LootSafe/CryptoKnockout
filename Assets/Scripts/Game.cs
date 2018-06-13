using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    
    public int rounds = 3;
    public int lives = 2;

    private static Game instance;
    private static Player localPlayer;


    void Start()
    {
        instance = this;
        localPlayer = new Player(new TestCharacter());
    }

    public int GetLives()
    {
        return lives;
    }
    
    public int GetRounds()
    {
        return rounds;
    }
    public static Player GetPlayer()
    {
        return localPlayer;
    }

    public static Game GetInstance()
    {
        return instance;
    }

    void Update()
    {

    }


}
