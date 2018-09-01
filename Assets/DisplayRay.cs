using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRay : MonoBehaviour {

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(GetComponentInParent<Transform>().position, forward, Color.green);
    }

}
