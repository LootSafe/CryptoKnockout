using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revolver : MonoBehaviour
{

    private float RotateSpeed = 5f;
    private float Radius = 20f;
    public RectTransform centerObject;
    private Vector2 _centre;
    private float _angle;
    public float overrideRadius = 0;

    private void Start()
    {
    }

    private void Update()
    {
        _centre = centerObject.position;
        Radius = GetComponentInParent<RectTransform>().sizeDelta.x - (GetComponent<RectTransform>().sizeDelta.x/2) ;
        if(overrideRadius!= 0)
        {
            Radius = overrideRadius;
        }
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        GetComponent<RectTransform>().position = _centre + offset;
    }



}
