using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnLocalPlay()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void BtnMultiplayerPlay()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}
