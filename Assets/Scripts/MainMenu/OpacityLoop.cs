using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpacityLoop : MonoBehaviour {

    public float speed = 0.05f;
    float direction = 1;
    public float maxAlpha = 1f;
    public float minAlpha = 0.2f;
    public MaskableGraphic graphic;
    float lastAlpha = 1;
	
	// Update is called once per frame
	void Update () {
        if (!graphic) return;
        Debug.Log("Alpha: " + graphic.color.a);
        if (lastAlpha > maxAlpha || lastAlpha < minAlpha) direction*= -1;
        float newAlpha = lastAlpha + speed * direction;
        lastAlpha = graphic.color.a;
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, newAlpha);

        
    }
}
