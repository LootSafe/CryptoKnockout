using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarUpdator : MonoBehaviour {

    public InputHandler inputHandler;
    public int playerNumber;

    GameObject p1Selection, p2Selection;
	// Use this for initialization
	void Start () {
        p1Selection = inputHandler.p1Selection;
        p2Selection = inputHandler.p2Selection;
	}
	
	// Update is called once per frame
	void Update () {
        if(playerNumber == 1)
        {
            if (p1Selection)
            {
                Sprite sprite = p1Selection.GetComponent<CharacterSelectButtons>().avatar;
                GetComponent<Image>().sprite = sprite;
            }
        }
        else if(playerNumber == 2)
        {
            if (p2Selection)
            {
                Sprite sprite = p2Selection.GetComponent<CharacterSelectButtons>().avatar;
                GetComponent<Image>().sprite = sprite;
            }
        }

	}
}
