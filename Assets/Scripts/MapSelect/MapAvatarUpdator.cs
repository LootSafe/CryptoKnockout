using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAvatarUpdator : MonoBehaviour {

    public MapSelectHandler inputHandler;
    public GameObject defaultSelection;

    GameObject selectedMap;
	// Use this for initialization
	void Start () {
        selectedMap = defaultSelection;
	}
	
	// Update is called once per frame
	void Update () {
        selectedMap = inputHandler.selectedMap;
        if (selectedMap && selectedMap.GetComponent<MapSelectButton>())
        {
            Sprite sprite = selectedMap.GetComponent<MapSelectButton>().avatar;
            GetComponent<Image>().sprite = sprite;
                
        }


	}
}
