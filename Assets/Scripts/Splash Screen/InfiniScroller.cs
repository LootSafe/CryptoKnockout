using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniScroller : MonoBehaviour {
    Image image;
    public float speed = 0.5f;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 offset = new Vector2(Time.time * speed, 0);
        image.material.mainTextureOffset = offset;
	}
}
