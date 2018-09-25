using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectFollower : MonoBehaviour {
    public RectTransform followObject;

    public RectTransform healthFill;
    public RectTransform damageFill;

    private RectTransform rect;
    public ParticleSystem particles;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        particles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = particles.shape.scale;
        Vector3 position = particles.shape.position; 
        var clone = particles.shape;
        var emClone = particles.emission;
        rect.position = followObject.position;
        //particles.shape.position;

        //damageFill.sizeDelta.x 
        clone.scale = new Vector3(Mathf.Abs(damageFill.sizeDelta.x - healthFill.sizeDelta.x) + 5, scale.y, scale.z);
        clone.position = new Vector3(-(particles.shape.scale.x / 2) + 25, position.y, position.z);
        emClone.rateOverTime = Mathf.CeilToInt(scale.x / 20) + 1;
	}
}
