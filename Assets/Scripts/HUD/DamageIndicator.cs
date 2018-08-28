using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour {
    public float life = 1f;
    float startTime;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
    
    public void Init(float Damage)
    {
        if (GetComponent<Text>())
        {
            GetComponent<Text>().text = ((int)Damage).ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime >= life)
        {
            Destroy(gameObject);
        }
	}
}
