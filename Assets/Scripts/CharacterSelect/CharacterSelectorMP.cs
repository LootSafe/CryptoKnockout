using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CharacterSelectorMP : NetworkBehaviour {


    public EventSystem player1E;
    public GameObject[] selected = new GameObject[2];
    public GameObject[] starts = new GameObject[2];
    public bool[] ready = new bool[2];

    //NW
    [SyncVar]
    float countDown;


    private float startDelayTimer;


    GlobalGameData data;

    private static CharacterSelectorMP instance;
    public static CharacterSelectorMP GetInstance() { return instance; }
    public GameObject startToContinueText;

    private InputLocker locker;

	// Use this for initialization
	void Start () {
        startDelayTimer = Time.time;
        selected[1] = starts[1];
        selected[1].GetComponent<CharacterSelectButtons>().Select();
        instance = this;
        data = GlobalGameData.GetInstance();
        locker = new InputLocker();
    }
	
	// Update is called once per frame
	void Update () {
        if (!(Time.time >= startDelayTimer + 0.5)) return;
        UpdateEscape();
        CheckForInput(PlayerIndex.One);
        UpdateReady();
    }

    void UpdateEscape()
    {
        
        if (GamePad.GetState(PlayerIndex.One).Released(CButton.B) && !ready[1])
        {
            Back();
        }
            
    }
    void UpdateReady()
    {
        
        if (ready[1] && ready[2])
        {
            startToContinueText.SetActive(true);

            if (GamePad.GetState().Released(CButton.B))
            {
                ready[1] = false;
                ready[2] = false;
            }


            if (GamePad.GetState().Released(CButton.A) || GamePad.GetState().Released(CButton.Start))
            {
                    NextScene();
            }
            else
            {
            }


        }
        else
        {
            startToContinueText.SetActive(false);
        }
    }

    void CheckForInput(PlayerIndex pi)
    {


        GameObject s = selected[(int)pi];
        if (!s) s = starts[(int)pi];
        CharacterSelectButtons b = s.GetComponent<CharacterSelectButtons>();
        if (!b) return;
        if (!b.interactable) s = starts[(int)pi];

        Control<CAxis> cHor = new Control<CAxis>(CAxis.LX, pi);
        Control<CAxis> cVer = new Control<CAxis>(CAxis.LY, pi);

        GamePadState pad = GamePad.GetState(pi);
    
        float horizontal = GamePad.GetAxis(cHor);
        float vertical = GamePad.GetAxis(cVer);

        

        if (!ready[(int)pi])
        {
            //HORIZONTAL
            if (horizontal != 0)
            {
                if (!Locked(cHor))
                {
                    if (horizontal < 0)
                    {
                        GameObject swap = s.GetComponent<CharacterSelectButtons>().left;
                        if (swap && swap.GetComponent<CharacterSelectButtons>() && swap.GetComponent<CharacterSelectButtons>().interactable) s = swap;
                        Lock(cHor);
                    }
                    else if (horizontal > 0)
                    {
                        GameObject swap = s.GetComponent<CharacterSelectButtons>().right;
                        if (swap && swap.GetComponent<CharacterSelectButtons>() && swap.GetComponent<CharacterSelectButtons>().interactable) s = swap;
                        Lock(cHor);
                    }
                }
            }
            else
            {
                Unlock(cHor);
            }

            //VERTICAL
            if (vertical != 0)
            {
                if (!Locked(cVer))
                {
                    if (vertical < 0)
                    {
                        GameObject swap = s.GetComponent<CharacterSelectButtons>().down;
                        if (swap && swap.GetComponent<CharacterSelectButtons>() && swap.GetComponent<CharacterSelectButtons>().interactable) s = swap;
                        Lock(cVer);
                    }
                    else if (vertical > 0)
                    {
                        GameObject swap = s.GetComponent<CharacterSelectButtons>().up;
                        if (swap && swap.GetComponent<CharacterSelectButtons>() && swap.GetComponent<CharacterSelectButtons>().interactable) s = swap;
                        Lock(cVer);
                    }
                }
            }
            else
            {
                Unlock(cVer);
            }



            //Submit Selection

            if (selected[(int)pi] != s)
            {
                selected[(int)pi].GetComponent<CharacterSelectButtons>().Deselect();
                s.GetComponent<CharacterSelectButtons>().Select();
            }
            selected[(int)pi] = s;


            //Check For Continue
            if (pad.Released(CButton.A))
            {
                    if (pi == PlayerIndex.One)
                    {
                        data.player1Char = selected[(int)pi].GetComponent<CharacterSelectButtons>().character;
                        ready[(int)pi] = true;
                    }
                    else if (pi == PlayerIndex.Two)
                    {
                        data.player2Char = selected[(int)pi].GetComponent<CharacterSelectButtons>().character;
                        ready[(int)pi] = true;
                    }
                }

        }
        else
        {
            if (pad.Released(CButton.B))
            {
                ready[(int)pi] = false;
            }
        }


       
    }


    public void NextScene()
    {
        SceneManager.LoadScene("MapSelect");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool Locked(Control<CAxis> control)
    {
        return locker.HasLock(control);
    }

    public void Unlock(Control<CAxis> control)
    {
        locker.Unlock(control);
    }

    public void Lock(Control<CAxis> control)
    {
        locker.Lock(control);
    }

    public bool Locked(Control<CButton> control)
    {
        return locker.HasLock(control);
    }

    public void Unlock(Control<CButton> control)
    {
        locker.Unlock(control);
    }

    public void Lock(Control<CButton> control)
    {
        locker.Lock(control);
    }


    public GameObject[] GetSelected()
    {
        return selected;
    }

    
}
