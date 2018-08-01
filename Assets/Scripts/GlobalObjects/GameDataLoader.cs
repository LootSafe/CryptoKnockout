using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataLoader : MonoBehaviour {

    public GameObject GameDataPrefab;
	// Use this for initialization
	void Start () {
        if (!GlobalGameData.GetInstance())
        {
            Instantiate(GameDataPrefab);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
