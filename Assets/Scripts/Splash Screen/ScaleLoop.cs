using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleLoop : MonoBehaviour
{

    public float speed = 0.0001f;
    float direction = 1;
    public float maxMultiplier = 1.5f;
    public float minMultiplier = 0.9f;
    private  Vector2 orgSize;
    private RectTransform rect;
    

    void Start()
    {
        rect = GetComponent<RectTransform>();
        orgSize = rect.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = (maxMultiplier - minMultiplier) * Mathf.Sin(Time.time) + minMultiplier;
        Debug.Log(" Angled - " + multiplier + " Range:" + (maxMultiplier - minMultiplier) + " sin" + Mathf.Sin(Time.time));
        GetComponent<RectTransform>().sizeDelta = new Vector2(orgSize.x * multiplier, orgSize.y * multiplier);
    }
}
