using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    
    private bool cancelLock;
    private bool forwardLock;
    private bool submitLock;
    bool selected;
    public GameObject selectedObject;
    public GameObject lastSelection;

    public GameObject p1Selection;
    public GameObject p2Selection;

    public RectTransform reticule;
    private State state;
    EventSystem e;

    public StandaloneInputModule p1;
    public StandaloneInputModule p2;

    void Start()
    {
        e = GetComponent<EventSystem>();
    }
	// Update is called once per frame
	void Update () {
        string stateName = "";
        switch (state)
        {
            case State.p1:
                stateName = "P1_";
                p1.enabled = true;
                p2.enabled = false;
                break;
            case State.p2:
                p1.enabled = false;
                p2.enabled = true;
                stateName = "P2_";
                break;
            default:
                stateName = "P1_";
                p1.enabled = true;
                p2.enabled = false;
                break;
        }
        
        if (!e.currentSelectedGameObject)
        {
            selected = false;
        }
        //Activate Selection
        if((Input.GetAxisRaw(stateName + "Vertical") != 0 || Input.GetAxisRaw(stateName + "Horizontal") != 0) && !selected)
        {
            e.SetSelectedGameObject(selectedObject);
            reticule.gameObject.SetActive(true);
            selected = true;
        }

        GameObject o = e.currentSelectedGameObject;
        if (o)
        {
            if (lastSelection != o)
            {

                if (o.GetComponent<CharacterSelectButtons>())
                {
                    reticule.gameObject.SetActive(true);
                    reticule.position = o.GetComponent<RectTransform>().position;
                    lastSelection = o;
                }
                else
                {
                    reticule.gameObject.SetActive(false);
                }
            }
        }

        UpdateSelection();
        
        
        

	}

    public void UpdateSelection()
    {
        if (Input.GetAxisRaw("Submit") != 0)
        {
            if (!submitLock)
            {
                submitLock = true;
                switch (state)
                {
                    case State.p1:
                        p1Selection = e.currentSelectedGameObject;
                        state = State.p2;
                        break;
                    case State.p2:
                        p2Selection = e.currentSelectedGameObject;
                        state = State.done;
                        break;
                    case State.done:
                        break;
                }
            }
        }
        else
        {
            submitLock = false;
        }

        if (Input.GetAxis("Cancel") > 0 && !cancelLock)
        {
            cancelLock = true;

            if (!cancelLock)
            {
                cancelLock = true;
                switch (state)
                {
                    case State.p1:
                        SceneManager.LoadScene("MainMenu");
                        break;
                    case State.p2:
                        state = State.p1;
                        break;
                    case State.done:
                        state = State.p2;
                        break;
                }
            }
        } else
        {
            cancelLock = false;
        }


    }

    void Back()
    {

    }

    void NextScene()
    {
        SceneManager.LoadScene("MapSelect");
    }

    enum State
    {
        p1 = 1,
        p2 = 2,
        done = 3
    }


}
