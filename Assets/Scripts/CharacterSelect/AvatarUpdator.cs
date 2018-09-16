using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarUpdator : MonoBehaviour {

    public CharacterSelector selections;
    public GameObject ReadyIndicator;
    public int playerNumber;
    public Text text;

    public Color readyColor;

    GameObject p1Selection, p2Selection;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        p1Selection = selections.GetSelected()[1];
        p2Selection = selections.GetSelected()[2];

        

        if (playerNumber == 1)
        {
            if (p1Selection && p1Selection.GetComponent<CharacterSelectButtons>())
            {
                Sprite sprite = p1Selection.GetComponent<CharacterSelectButtons>().avatar_left;
                GetComponent<Image>().sprite = sprite;
                text.text = p1Selection.GetComponent<CharacterSelectButtons>().character.ToString();
                text.color = Color.white;

            }

            if (selections.ready[1])
            {
                ReadyIndicator.SetActive(false);
                text.text = "READY";
                text.color = readyColor;
            }
            else
            {
                ReadyIndicator.SetActive(true);
            }
        }
        else if(playerNumber == 2)
        {
            if (p2Selection && p2Selection.GetComponent<CharacterSelectButtons>())
            {

                Sprite sprite = p2Selection.GetComponent<CharacterSelectButtons>().avatar_right;
                GetComponent<Image>().sprite = sprite;
                text.text = p2Selection.GetComponent<CharacterSelectButtons>().character.ToString();
                text.color = Color.white;
            }

            if (selections.ready[2])
            {
                ReadyIndicator.SetActive(false);
                text.text = "READY";
                text.color = readyColor;
            }
            else
            {
                ReadyIndicator.SetActive(true);
            }
        }

	}
}
