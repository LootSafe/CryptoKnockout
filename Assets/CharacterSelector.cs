using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterSelector : MonoBehaviour {


    public EventSystem player1E;
    public EventSystem player2E;
    public GameObject[] selected = new GameObject[9];
    public GameObject[] starts = new GameObject[9];
    public bool[] ready = new bool[9];
    private InputLocker locker;

    GlobalGameData data;

    private static CharacterSelector instance;
    public static CharacterSelector GetInstance() { return instance; }
    public GameObject startToContinueText;

	// Use this for initialization
	void Start () { 
        selected[1] = starts[1];
        selected[1].GetComponent<CharacterSelectButtons>().Select();
        selected[2] = starts[2];
        selected[2].GetComponent<CharacterSelectButtons>().Select();
        locker = new InputLocker();
        instance = this;
        data = GlobalGameData.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
        CheckForInput(PlayerIndex.One);
        CheckForInput(PlayerIndex.Two);
        UpdateReady();
    }

    void UpdateReady()
    {
        if(ready[1] && ready[2])
        {
            startToContinueText.SetActive(true);

            if (GamePad.GetButton(CButton.B))
            {
                ready[1] = false;
                ready[2] = false;
            }

            if (GamePad.GetButton(CButton.A) || GamePad.GetButton(CButton.Start))
            {
                NextScene();
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
                        Locked(cVer);
                    }
                }
            }
            else
            {
                Unlock(cVer);
            }





            if (selected[(int)pi] != s)
            {
                selected[(int)pi].GetComponent<CharacterSelectButtons>().Deselect();
                s.GetComponent<CharacterSelectButtons>().Select();
            }
            selected[(int)pi] = s;

            if (GamePad.GetButton(CButton.A, pi))
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
            if (GamePad.GetButton(CButton.B, pi))
            {
                ready[(int)pi] = false;
            }
        }


       
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

    public GameObject[] GetSelected()
    {
        return selected;
    }

    class InputLocker
    {

        private float delay = 1f;
        bool[,] axisLocks = new bool[9,8];
        float[,] axisTimes = new float[9,8];

        public bool HasLock(Control<CAxis> control)
        {
            if (control.pi == PlayerIndex.Any) return false;
            return axisLocks[(int)control.pi, (int)control.control];
        }

        public void Unlock(Control<CAxis> control)
        {

            if (control.pi == PlayerIndex.Any) return;
            axisLocks[(int)control.pi, (int)control.control] = false;
        }

        public void Lock(Control<CAxis> control)
        {
            if (control.pi == PlayerIndex.Any) return;
            axisLocks[(int)control.pi, (int)control.control] = true;
        }
    }


    public void NextScene()
    {
        SceneManager.LoadScene("MapSelect");
    }
}
