using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float dampTime = 0.15f;
    public float offSetY = 0.2f;

    const float center = 0.5f;

    Vector3 velocity = Vector3.zero;
    Camera cam;
    GameObject target = null;

    void Start()
    {
        cam = Camera.main;    
    }

    void Update()
    {
        if(target == null)
        {

        }
        else
        {
            Vector3 point = cam.WorldToViewportPoint(transform.position);
            Vector3 delta = transform.position - cam.ViewportToWorldPoint(new Vector3(center, center + offSetY, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}