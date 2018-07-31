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
    public bool randomize;
    private  Vector2 orgSize;
    private RectTransform rect;
    private float randomFactor;
    public float frequency = 1;
    

    void Start()
    {
        rect = GetComponent<RectTransform>();
        orgSize = rect.sizeDelta;
        if (randomize)
        {
            randomFactor = Random.Range(0.1f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = (maxMultiplier - minMultiplier) * Mathf.Sin(frequency * Time.time + randomFactor) + minMultiplier;
        GetComponent<RectTransform>().sizeDelta = new Vector2(orgSize.x * multiplier, orgSize.y * multiplier);
    }
}
