using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 15f;

        transform.position = new Vector3(transform.position.x + x, transform.position.y + z, transform.position.z + z);

    }
}