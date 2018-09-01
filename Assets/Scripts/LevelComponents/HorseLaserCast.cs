using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseLaserCast : MonoBehaviour {

    public LaserRandomScript laser;
    public Vector2 randomOffsetMin;
    public Vector2 randomOffsetMax;

    public LineRenderer line;
    public Transform damageSource;

    public bool debug;

    void Start()
    {
        
    }
	void Update () {
        damageSource.gameObject.SetActive(false);
        Vector2 offsets = new Vector2(Random.Range(randomOffsetMin.x, randomOffsetMax.x), 
                                      Random.Range(randomOffsetMin.y, randomOffsetMax.y));

        Vector2 direction = (Vector2)laser.GetCurrentTarget().position + offsets;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction , 50.0f);
        damageSource.gameObject.SetActive(true);
        if (debug)
        {
            Debug.DrawRay(transform.position, direction * 100, Color.green);
            Debug.Log("Target:" + laser.GetCurrentTarget().transform.parent.name);
        }

        if (hit.collider != null)
        {
            Transform objectHit = hit.transform;
            if(debug) Debug.Log("I Hit" + objectHit.parent.name);
            UpdateLaserDrawing(objectHit.position);
            damageSource.position = objectHit.position;
            
        }
    }

    void UpdateLaserDrawing(Vector2 end)
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector3)end);

    }
}
