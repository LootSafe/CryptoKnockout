using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSelectHandler : MonoBehaviour
{


    private bool cancelLock;
    private bool forwardLock;
    private bool submitLock;
    bool selected;
    public GameObject selectedObject;
    public GameObject lastSelection;

    public GameObject selectedMap;

    public Button nextSceneButton;

    public RectTransform reticule;
    string stateName = "P1_";
    EventSystem e;

    public StandaloneInputModule p1;
    public StandaloneInputModule p2;

    public GlobalGameData data;

    void Start()
    {
        e = GetComponent<EventSystem>();
        e.SetSelectedGameObject(selectedObject);
    }
    // Update is called once per frame
    void Update()
    {

        if (!data)data = GlobalGameData.GetInstance();
        if (!e.currentSelectedGameObject)
        {
            selected = false;
        }
        //Activate Selection
        if ((Input.GetAxisRaw(stateName + "Vertical") != 0 || Input.GetAxisRaw(stateName + "Horizontal") != 0) && !selected)
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

                if (o.GetComponent<MapSelectButton>())
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
        if (Input.GetAxisRaw(stateName + "Punch") != 0)
        {
            if (!submitLock)
            {
                submitLock = true;
                if (e.currentSelectedGameObject.GetComponent<MapSelectButton>())
                {
                    selectedMap = e.currentSelectedGameObject;
                    data.selectedLevel = selectedMap.GetComponent<MapSelectButton>().sceneName;
                    
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
                Back();

            }
        }
        else
        {
            cancelLock = false;
        }


    }

    public void Back()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(data.selectedLevel);
    }



}
