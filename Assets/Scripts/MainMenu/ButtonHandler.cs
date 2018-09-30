using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    bool escapeLock;
	// Use this for initialization
	void Start () {
        escapeLock = false;		
	}
	
	// Update is called once per frame
	void Update () {
        //Escape Key Handling
        if (GamePad.GetButton(CButton.Back) && !escapeLock)
        {
            escapeLock = true;
            SceneManager.LoadScene("Intro Screen");
        } else
        {
            escapeLock = false;
        }
	}

    public void BtnLocalPlay()
    {

        GlobalGameData.GetInstance().selectedGameMode = Game.GameMode.LOCALMULTIPLAYER;
        SceneManager.LoadScene("CharacterSelect");
    }

    public void BtnMultiplayerPlay()
    {
        GlobalGameData.GetInstance().selectedGameMode = Game.GameMode.NETWORKMULTIPLAYER;
        SceneManager.LoadScene("MPSelect");
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}
