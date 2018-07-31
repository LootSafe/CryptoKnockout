using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    
    bool cancelLock;
    bool forwardLock;
    bool selected;
    public GameObject selectedObject;
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Cancel") > 0 && !cancelLock)
        {
            cancelLock = true;
            SceneManager.LoadScene("MainMenu");
        }

        //Activate Selection
        if(Input.GetAxisRaw("Vertical") != 0 && !selected)
        {
            GetComponent<EventSystem>().SetSelectedGameObject(selectedObject);
            selected = true;
        }

	}

    public void onDisable()
    {
    }

    void Back()
    {

    }

    void Next()
    {

    }

    enum state
    {
        p1 = 1,
        p2 = 2,
        done = 3
    }


}
