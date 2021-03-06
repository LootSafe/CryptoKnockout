﻿using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float dampTime = 1.00f;
    public float offSetY = 2.0f;

    const float center = 0.5f;

    Vector3 velocity = Vector3.zero;
    Vector3 lastPosition;
    Camera cam;
    GameObject target = null;

    void Start()
    {
        cam = Camera.main;
        target = gameObject;
        lastPosition = new Vector3();
    }

    void Update()
    { 
        if (target != null)
        {
            Vector3 point = cam.WorldToViewportPoint(target.transform.position);
            Vector3 delta = transform.position - cam.ViewportToWorldPoint(new Vector3(center, center, point.z));
            Vector3 destination = target.transform.position + delta;

            if(transform.position != lastPosition)
            {
                Vector3 bestPosition = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
                cam.transform.position = new Vector3(bestPosition.x, bestPosition.y += offSetY, -10.0f);
                lastPosition = transform.position;
            } 
        }
    }
}