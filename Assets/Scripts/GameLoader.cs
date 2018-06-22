using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public GameObject game;
    public GameObject soundManager;
    


    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (Game.GetInstance() == null) Instantiate(game);

        game.SetActive(true);
            
    }
}
