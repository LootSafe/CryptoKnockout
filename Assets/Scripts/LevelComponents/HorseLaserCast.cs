using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseLaserCast : MonoBehaviour {

    public LaserRandomScript laser;
    public Transform laserSource;

    public Transform lineTestObject;

    public Vector2 randomOffsetMin;
    public Vector2 randomOffsetMax;

    public Vector2 directionOverride;

    public Transform particles;

    public LineRenderer line;

    public bool debug;

    void Start()
    {
        
    }
	void Update () {
        //Make Sure ine Isn' Visible till Sized
        line.enabled = false;
        Vector2 offsets = new Vector2(Random.Range(randomOffsetMin.x, randomOffsetMax.x), 
                                      Random.Range(randomOffsetMin.y, randomOffsetMax.y));

        Vector2 direction = (Vector2)laser.GetNextPoint() ;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction - (Vector2)transform.position , 50.0f);
        
        if (debug)
        {
            Debug.DrawRay(transform.position, (direction - (Vector2)transform.position) * 20  , Color.green);
            //Debug.Log("Target:" + laser.GetCurrentTarget().transform.parent.name);
        }

        if (hit.collider != null)
        {
            Transform objectHit = hit.transform;            
            //if(debug) Debug.Log("I Hit" + objectHit.parent.name);
            UpdateLaserDrawing(hit.point);

            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Player>().TakeDamage(1, null);
            }
            
            
        }
    }

    void UpdateLaserDrawing(Vector2 end)
    {
        line.enabled = true;
        Vector3[] points = new Vector3[2];
        points[0] = laserSource.position;
        points[1] = new Vector3(end.x, end.y, 0);
        particles.position = end;

        line.SetPositions(points);

        if (debug)
        {
            //lineTestObject.gameObject.SetActive(true);
            lineTestObject.position = laserSource.position;
            //Debug.Log("Line A:" + (Vector2)laserSource.position + " to " + end);
            //Debug.Log("Line B:" + (Vector2)line.GetPosition(0) + " to " + (Vector2)line.GetPosition(1));
            //Debug.Assert(laserSource.position == line.GetPosition(0));
            //Debug.Assert((end == (Vector2)line.GetPosition(1)));
        }

    }

    void ResetLine()
    {
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }

    void UpdateParticleSystem(Vector2 end)
    {

    }
}
