using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectButtons : MonoBehaviour {
    public RectTransform reticule;
    private bool selected;
    Button button;
    public Sprite avatar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
