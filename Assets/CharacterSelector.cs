using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CharacterSelector : MonoBehaviour {


    public EventSystem player1E;
    public EventSystem player2E;
    private GameObject[] selected;
    public GameObject[] starts = new GameObject[8];
    private InputLocker locker;

    private static CharacterSelector instance;
    public static CharacterSelector GetInstance() { return instance; }

	// Use this for initialization
	void Start () {
        selected = new GameObject[8];
        selected[0] = starts[0];
        selected[0].GetComponent<CharacterSelectButtons>().Select();
        selected[1] = starts[1];
        selected[1].GetComponent<CharacterSelectButtons>().Select();
        locker = new InputLocker();
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        CheckForInput(PlayerIndex.One);
        CheckForInput(PlayerIndex.Two);
	}

    void CheckForInput(PlayerIndex pi)
    {
        //Horizontal
        GameObject s = selected[(int)pi - 1];
        CharacterSelectButtons b = s.GetComponent<CharacterSelectButtons>();
        if (!b) return;
        if (!b.interactable) s = starts[(int)pi - 1];

        Control<CAxis> cHor = new Control<CAxis>(CAxis.LX, pi);
        Control<CAxis> cVer = new Control<CAxis>(CAxis.LX, pi);


        float horizontal = GamePad.GetAxis(CAxis.LX, pi);
        float vertical = GamePad.GetAxis(CAxis.LY, pi);

        //HORIZONTAL
        if(horizontal != 0)
        {
            if(!Locked(CAxis.LX, pi))
            {
                if (horizontal < 0)
                {
                    GameObject swap = s.GetComponent<CharacterSelectButtons>().left;
                    if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
                    Lock(CAxis.LX, pi);
                }
                else if (horizontal > 0)
                {
                    GameObject swap = s.GetComponent<CharacterSelectButtons>().right;
                    if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
                    Lock(CAxis.LX, pi);
                }
            }
        } 
        else
        {
            Unlock(CAxis.LX, pi);
        }

        //VERTICAL
        if (vertical != 0)
        {
            if(!Locked(CAxis.LY, pi))
            {
                if (vertical < 0 && !verticalLock)
                {
                    GameObject swap = s.GetComponent<CharacterSelectButtons>().down;
                    if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
                    Lock(CAxis.LY, pi);
                }
                else if (vertical > 0 && !verticalLock)
                {
                    GameObject swap = s.GetComponent<CharacterSelectButtons>().up;
                    if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
                    Locked(CAxis.LY, pi);
                }
            }
        }
        else
        {
            Unlock(CAxis.LY, pi);
        }

        if (selected[(int)pi - 1] != s)
        {
            selected[(int)pi - 1].GetComponent<CharacterSelectButtons>().Deselect();
            s.GetComponent<CharacterSelectButtons>().Select();
        }
        selected[(int)pi - 1] = s;
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
            axisLocks[(int)control.pi, (int)control.control] = false;
        }
    }
}
