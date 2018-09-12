using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour {
    private Game game;
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

    public void Resume()
    {
        game.UnPause();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
