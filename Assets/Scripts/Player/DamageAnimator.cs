using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimator : MonoBehaviour {
    float lastHit;
    public float animateSpeed = 10;
    public float hueIntensity = .4f;
    public int numberOfMarkers = 5;
    public float duration = 0.2f;
    public DamageIndicatorController damageIndicator;

    float[,] damageQuantities;
    Color orgColor;
	// Use this for initialization
	void Start () {
        orgColor = GetComponent<SpriteRenderer>().color;
        damageQuantities = new float[numberOfMarkers, 2];
	}

    public void TriggerSmallHit(float damage, Player source)
    {
        lastHit = Time.time;
        damageIndicator.TriggerIndicator(damage, source);
    }

    public void AddMarker()
    {

    }
	


    void UpdateSaturation()
    {
        float saturation = ((Time.time - lastHit) / duration);
        GetComponent<SpriteRenderer>().color = new Color(1, saturation, saturation);
    }

    void UpdateMarkers()
    {

    }



    // Update is called once per frame
    void Update()
    {
        UpdateSaturation();
        UpdateMarkers();
    }
}
