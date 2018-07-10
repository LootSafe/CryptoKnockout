using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpacityLoop : MonoBehaviour {

    public float speed = 0.0001f;
    float direction = 1;
    public float maxAlpha = 1f;
    public float minAlpha = 0.2f;
    public float delay = 0.066f;
    public MaskableGraphic graphic;

    float lastUpdate;

    void Start()
    {
        lastUpdate = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (!graphic) return;
        if (Time.time - lastUpdate > delay)
        {
            float newAlpha = graphic.color.a + speed * direction;
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, newAlpha);
            lastUpdate = Time.time;
            if (graphic.color.a >= maxAlpha || graphic.color.a <= minAlpha) direction *= -1;
        }
    }
}
