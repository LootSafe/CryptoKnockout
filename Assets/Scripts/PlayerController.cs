using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    Player player;
    float lastHeading = 1;
    void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        //Run For Local Player
        if (!isLocalPlayer) return;

        //Get Inputs
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        Debug.Log("Jump" + jump);
        
        //Horizontal Changes
        if (xMovement != 0)
        {
            //vars
            Debug.Log(xMovement);
            Vector3 rotation = transform.localScale;
            Vector3 updatedHeading = rotation;
            float quotient = xMovement / Mathf.Abs(xMovement);
            //Rotation
            if (quotient != lastHeading)
            {
                updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                lastHeading = quotient;
            }
            //Horizontal Movement
            transform.position = new Vector3(transform.position.x + (xMovement * player.GetMoveSpeed()), transform.position.y, transform.position.z);
            transform.localScale = updatedHeading;
        }

        //Vertical Changes
        if(jump > 0)
        {
            //Vertical movement
            transform.position = new Vector3(transform.position.x , transform.position.y + (player.GetMoveSpeed() *yMovement), transform.position.z);
        }

        
    }
}