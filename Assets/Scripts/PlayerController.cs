using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    float lastHeading = 1;
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 15f;

        
        
        if (x != 0)
        {
            //Rotation
            Vector3 rotation = transform.localScale;
            Vector3 updatedHeading = rotation;
            float quo = x / Mathf.Abs(x);
            if (quo != lastHeading)
            {
                updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                lastHeading = quo;
            }
            //Horizontal Movement
            transform.position = new Vector3(transform.position.x + x, transform.position.y + z, transform.position.z + z);
            transform.localScale = updatedHeading;
        }

        
    }
}