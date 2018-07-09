using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerPlayerController : MonoBehaviour {

    float p1lastHeading = 1;
    float p2lastHeading = 1;
    Game game;
    Player player1;
    Player player2;
    Transform p1Transform;
    Transform p2Transform;


    // Use this for initialization
    void Start () {
        game = Game.GetInstance();
        Debug.Log("Starting Up Game Controller");
	}

    // Update is called once per frame
    void Update() {
        //Run Only if Local Multiplayer
        if (!game) return;
        if (game.GetGameMode() != Game.GameMode.LOCALMULTIPLAYER)
        {
            Debug.Log("Game is not in Local Multiplayer Mode");
            Destroy(this);
            return;
        }

        if (!player1 || !player2)
        {
            player1 = game.GetPlayer(0);
            if (player1)
            {
                p1Transform = player1.GetComponentInParent<Transform>();
            }
            player2 = game.GetPlayer(1);
            if (player2)
            {
                p2Transform = player2.GetComponentInParent<Transform>();
            }

        }

        if (player1) {
            /*********************************************/
            //Player 1 Get Inputs
            float xMovement = Input.GetAxis("P1_Horizontal");
            float yMovement = Input.GetAxis("P1_Vertical");
            float jump = Input.GetAxis("P1_Jump");

            //Horizontal Changes
            if (xMovement != 0)
            {
                //vars
                Vector3 rotation = p1Transform.localScale;
                Vector3 updatedHeading = rotation;
                float quotient = xMovement / Mathf.Abs(xMovement);
                //Rotation
                if (quotient != p1lastHeading)
                {
                    updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                    p1lastHeading = quotient;
                }
                //Horizontal Movement
                p1Transform.position = new Vector3(p1Transform.position.x + (xMovement * player1.GetMoveSpeed()), p1Transform.position.y, p1Transform.position.z);
                p1Transform.localScale = updatedHeading;
            }

            //Vertical Changes
            if (jump > 0)
            {
                //Vertical movement
                p1Transform.position = new Vector3(p1Transform.position.x, p1Transform.position.y + (player1.GetMoveSpeed() * jump), p1Transform.position.z);
            }
        }


        if (player2)
        {
            /*********************************************/
            //Player 2 Get Inputs
            float p2xMovement = Input.GetAxis("P2_Horizontal");
            float p2yMovement = Input.GetAxis("P2_Vertical");
            float p2jump = Input.GetAxis("P2_Jump");

            //Horizontal Changes
            if (p2xMovement != 0)
            {
                //vars
                Vector3 rotation = p2Transform.localScale;
                Vector3 updatedHeading = rotation;
                float quotient = p2xMovement / Mathf.Abs(p2xMovement);
                //Rotation
                if (quotient != p2lastHeading)
                {
                    updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                    p2lastHeading = quotient;
                }
                //Horizontal Movement
                p2Transform.position = new Vector3(p2Transform.position.x + (p2xMovement * player2.GetMoveSpeed()), p2Transform.position.y, p2Transform.position.z);
                p2Transform.localScale = updatedHeading;
            }

            //Vertical Changes
            if (p2jump > 0)
            {
                //Vertical movement
                p2Transform.position = new Vector3(transform.position.x, p2Transform.position.y + (player2.GetMoveSpeed() * p2jump), p2Transform.position.z);
            }
        }

    }
}

