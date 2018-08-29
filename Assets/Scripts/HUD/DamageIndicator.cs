using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour {
    public float life = 1f;
    float startTime;
    public Text textComponent;
    private bool critical = false;
    // Use this for initialization
    void Start() {
        startTime = Time.time;
        critical = true;
        if (!textComponent)
        {
            textComponent = GetComponent<Text>();
            critical = false;
        }
	}
    
    public void Init(float Damage, GameObject player)
    {
        if (critical)
        {
            textComponent.text = player.GetComponent<Player>().RequestHitWord();
        } else
        {
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
