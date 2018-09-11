using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectFollower : MonoBehaviour {
    public RectTransform followObject;
    private RectTransform rect;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
        rect.position = followObject.position;
	}
}
