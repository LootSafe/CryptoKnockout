using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectButtons : MonoBehaviour {
    private bool selected;
    Button button;
    public Sprite avatar_left;
    public Sprite avatar_right;
    public Character.Characters character;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public GameObject selectionIndicator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            selectionIndicator.SetActive(true);
        }
	}

    void OnMouseEnter()
    {
        selected = true;
    }
    void OnMouseExit()
    {
        selected = false;
    }

    void OnClick()
    {
        Debug.Log("Clicked");
    }

    
}
