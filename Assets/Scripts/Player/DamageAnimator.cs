using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimator : MonoBehaviour {
    float lastHit;
    public float animateSpeed = 10;
    public float hueIntensity = .4f;
    public float duration = 0.2f;
    Color orgColor;
	// Use this for initialization
	void Start () {
        orgColor = GetComponent<SpriteRenderer>().color;
	}

    public void TriggerSmallHit()
    {
        lastHit = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        float saturation = ((Time.time - lastHit) / duration);
        Debug.Log("Saturation:" + saturation);
        GetComponent<SpriteRenderer>().color = new Color(1, saturation, saturation);
	}
}
