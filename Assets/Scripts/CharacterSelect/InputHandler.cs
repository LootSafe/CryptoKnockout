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

    public Button nextSceneButton;

    public RectTransform reticule;
    public State state;
    string stateName = "";
    EventSystem e;

    public StandaloneInputModule p1;
    public StandaloneInputModule p2;

    GlobalGameData data;

    void Start()
    {
        e = GetComponent<EventSystem>();
        state = State.p1;
        e.SetSelectedGameObject(selectedObject);
        
    }
	// Update is called once per frame
	void Update () {
        if (!data) data = GlobalGameData.GetInstance();
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
        if(!e.currentSelectedGameObject && (Input.GetAxisRaw(stateName + "Vertical") != 0 || Input.GetAxisRaw(stateName + "Horizontal") != 0) && !selected)
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
        switch (state)
        {
            case State.p1:
                p1Selection = e.currentSelectedGameObject;
                break;
            case State.p2:
                p2Selection = e.currentSelectedGameObject;
                break;
        }


        if (Input.GetAxisRaw(stateName + "Punch") != 0)
        {
            if (!submitLock)
            {
                submitLock = true;
                switch (state)
                {
                    case State.p1:
                        p1Selection = e.currentSelectedGameObject;
                        if (p1Selection.GetComponent<Button>().IsInteractable())
                        {
                            data.player1Char = p1Selection.GetComponent<CharacterSelectButtons>().character;
                            state = State.p2;
                        }
                        break;
                    case State.p2:
                        p2Selection = e.currentSelectedGameObject;
                        if (p2Selection.GetComponent<Button>().IsInteractable())
                        {
                            data.player2Char = p2Selection.GetComponent<CharacterSelectButtons>().character;
                            nextSceneButton.interactable = true;
                            state = State.done;
                        }
                        break;
                    case State.done:
                        NextScene();
                        break;
                    default:
                        state = State.p1;
                    break;
                }
            }
        }
        else
        {
            submitLock = false;
        }

        if (Input.GetAxis(stateName + "Kick") > 0 && !cancelLock)
        {

            if (!cancelLock)
            {
                cancelLock = true;
                switch (state)
                {
                    case State.p1:
                        Back();
                        break;
                    case State.p2:
                        state = State.p1;
                        break;
                    case State.done:
                        state = State.p2;
                        nextSceneButton.interactable = false;
                        break;
                    default:
                        state = State.p1;
                        break;
                }
            }
        }
        else
        {
            cancelLock = false;
        }


    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MapSelect");
    }

    public enum State
    {
        p1 = 1,
        p2 = 2,
        done = 3
    }


}
