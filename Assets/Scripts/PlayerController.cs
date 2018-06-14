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
        
        var x = Input.GetAxis("Horizontal") * Time.deltaTime;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    }
}