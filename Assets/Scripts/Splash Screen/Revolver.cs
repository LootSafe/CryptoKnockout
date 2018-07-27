using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revolver : MonoBehaviour
{

    private float RotateSpeed = 5f;
    private float Radius = 20f;

    private Vector2 _centre;
    private float _angle;

    private void Start()
    {
        _centre = GetComponent<RectTransform>().position;
    }

    private void Update()
    {
        Radius = GetComponentInParent<RectTransform>().sizeDelta.x - (GetComponent<RectTransform>().sizeDelta.x/2) ;
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        GetComponent<RectTransform>().position = _centre + offset;
    }



}
