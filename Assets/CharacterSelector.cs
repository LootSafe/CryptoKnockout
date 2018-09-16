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
        selected[1] = starts[1];
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
        if (!s.GetComponent<CharacterSelectButtons>().interactable) s = starts[(int)pi - 1];
        CharacterSelectButtons b = s.GetComponent<CharacterSelectButtons>();
        if (!b) return;

        float horizontal = GamePad.GetAxis(CAxis.LX, pi);
        bool horizontalLock = Locked(CAxis.LX, pi);
        float vertical = GamePad.GetAxis(CAxis.LY, pi);
        bool verticalLock = Locked(CAxis.LY, pi);

        if (horizontal < 0 && !horizontalLock)
        {
            GameObject swap = s.GetComponent<CharacterSelectButtons>().left;
            if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
        }
        else if(horizontal > 0 && !horizontalLock)
        {
            GameObject swap = s.GetComponent<CharacterSelectButtons>().right;
            if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
        }
        else
        {
            Unlock(CAxis.LX, pi);
        }

        //Vertical
        if (vertical < 0 && !verticalLock)
        {
            GameObject swap = s.GetComponent<CharacterSelectButtons>().down;
            if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
        }
        else if (vertical > 0 && !verticalLock)
        {
            GameObject swap = s.GetComponent<CharacterSelectButtons>().up;
            if (swap && swap.GetComponent<CharacterSelectButtons>()) s = swap;
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

    public bool Locked(CAxis ca, PlayerIndex pi)
    {
        return locker.HasLock(ca, pi);
    }

    public void Unlock(CAxis ca, PlayerIndex pi)
    {
        locker.Unlock(ca, pi);
    }

    public GameObject[] GetSelected()
    {
        return selected;
    }

    class InputLocker
    {

        private float delay = 1f;
        bool[,] axisLocks = new bool[8,8];
        float[,] axisTimes = new float[8,8];

        public bool HasLock(CAxis ca, PlayerIndex pi)
        {

            if (axisLocks[(int)pi - 1, (int)ca])
            {
                return true;
            }
            else
            {
                axisLocks[(int)pi - 1, (int)ca] = true;
                return false;
                    
            }
        }

        public void Unlock(CAxis ca, PlayerIndex pi)
        {
            axisLocks[(int)pi - 1, (int)ca] = false;
        }

        public bool IsLocked(CAxis ca, PlayerIndex pi)
        {


            return true;
        }




    }
}
