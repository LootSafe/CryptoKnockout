using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    public GameObject game;
    public GameObject soundManager;
    public GlobalGameData globalDataPrefab;
    public GameObject escapeMenu;
    


    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (Game.GetInstance() == null)
        {
            //GameObject instance = Instantiate(game);
            //game.GetComponent<Game>().enabled = true;
            //game.GetComponent<Game>().escapeMenu = escapeMenu;
        }
        if (!GameObject.FindGameObjectWithTag("GlobalData"))
        {
            Instantiate(globalDataPrefab);
        }       

        game.SetActive(true);
            
    }
}
