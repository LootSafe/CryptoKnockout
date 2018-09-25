using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {
    public GameObject p1Tab;
    public GameObject p2Tab;
    public GameObject Root;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CharacterSelector.GetInstance().GetSelected()[1] == Root)
        {
            p1Tab.SetActive(true);
        }
        else
        {
            p1Tab.SetActive(false);
        }

        if (CharacterSelector.GetInstance().GetSelected()[2] == Root)
        {
            p2Tab.SetActive(true);
        }
        else
        {
            p2Tab.SetActive(false);
        }
    }
}
