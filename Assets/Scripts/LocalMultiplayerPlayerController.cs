using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerPlayerController : MonoBehaviour {

    float p1lastHeading = 1;
    float p2lastHeading = 1;
    Game game;
    Player player1;
    Player player2;


    // Use this for initialization
    void Start () {
        game = Game.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        //Run Only if Local Multiplayer
        if (game.GetGameMode() != Game.GameMode.LOCALMULTIPLAYER)
        {
            Destroy(this);
        }

        if (!player1 || !player2)
        {
            player1 = game.GetPlayer(0);
            player2 = game.GetPlayer(2);
        }    
        //Get Inputs
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        //Horizontal Changes
        if (xMovement != 0)
        {
            //vars
            Vector3 rotation = transform.localScale;
            Vector3 updatedHeading = rotation;
            float quotient = xMovement / Mathf.Abs(xMovement);
            //Rotation
            if (quotient != p1lastHeading)
            {
                updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                p1lastHeading = quotient;
            }
            //Horizontal Movement
            transform.position = new Vector3(transform.position.x + (xMovement * player.GetMoveSpeed()), transform.position.y, transform.position.z);
            transform.localScale = updatedHeading;
        }

        //Vertical Changes
        if (jump > 0)
        {
            //Vertical movement
            transform.position = new Vector3(transform.position.x, transform.position.y + (player.GetMoveSpeed() * jump), transform.position.z);
        }

        //Player 2 Get Inputs
        float p2xMovement = Input.GetAxis("P1_Horizontal");
        float p2yMovement = Input.GetAxis("P1_Vertical");
        float p2jump = Input.GetAxis("P1_p2jump");

        //Horizontal Changes
        if (p2xMovement != 0)
        {
            //vars
            Vector3 rotation = transform.localScale;
            Vector3 updatedHeading = rotation;
            float quotient = p2xMovement / Mathf.Abs(p2xMovement);
            //Rotation
            if (quotient != p2lastHeading)
            {
                updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                p2lastHeading = quotient;
            }
            //Horizontal Movement
            transform.position = new Vector3(transform.position.x + (p2xMovement * player.GetMoveSpeed()), transform.position.y, transform.position.z);
            transform.localScale = updatedHeading;
        }

        //Vertical Changes
        if (p2jump > 0)
        {
            //Vertical movement
            transform.position = new Vector3(transform.position.x, transform.position.y + (player.GetMoveSpeed() * p2jump), transform.position.z);
        }


    }
}

