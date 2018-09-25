using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuButtonSelector : MonoBehaviour {
    public EventSystem e;
    public Button b;

	// Use this for initialization
	void Start () {

    }
    void OnEnable()
    {
        e.SetSelectedGameObject(b.gameObject);    
    }



// Update is called once per frame
void Update () {
	    	
	}
}
