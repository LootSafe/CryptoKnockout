using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour {
    public float life = 1f;
    float startTime;
    public Text textComponent;

    // Use this for initialization
    void Start() {
        startTime = Time.time;  
    }
    
    public void Init(float Damage, Player player, bool critical, Player source)
    {
        if (critical)
        {   
            textComponent.text = source.RequestHitWord();
        } else
        {
            Debug.Log("That aint a crit");
            textComponent = GetComponent<Text>();
            textComponent.text = ((int)Damage).ToString();
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
