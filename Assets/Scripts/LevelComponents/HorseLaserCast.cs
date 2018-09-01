using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseLaserCast : MonoBehaviour {

    public LaserRandomScript laser;
    public Vector2 randomOffsetMin;
    public Vector2 randomOffsetMax;

    public bool debug;

	void Update () {
        Vector2 offsets = new Vector2(Random.Range(randomOffsetMin.x, randomOffsetMax.x), 
                                      Random.Range(randomOffsetMin.y, randomOffsetMax.y));

        Vector2 direction = (Vector2)laser.GetCurrentTarget().position + offsets;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, laser.GetCurrentTarget().position , 50.0f);

        if (debug)
        {
            Debug.DrawRay(transform.position, direction * 100, Color.green);
            Debug.Log("Target:" + laser.GetCurrentTarget().transform.parent.name);
        }

        if (hit.collider != null)
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit.parent.name);
        }
    }
}
