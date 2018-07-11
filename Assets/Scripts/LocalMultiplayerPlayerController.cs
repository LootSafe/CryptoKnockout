using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerPlayerController : MonoBehaviour {

    float[] lastHeadings;
    Game game;
    Player player1;
    Player player2;


    // Use this for initialization
    void Start () {
        game = Game.GetInstance();
        Debug.Log("Starting Up Game Controller");


        //Initialize Player Headings
        lastHeadings = new float[8];
        //P1
        lastHeadings[0] = 1;
        //P2
        lastHeadings[1] = -1;
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
            player2 = game.GetPlayer(1);
        }

        updatePlayer(player1, 1);
        updatePlayer(player2, 2);
    }

    private void updatePlayer(Player player, int playerNumber)
    {
        if (!player) return;
        
        Transform transform = player.GetComponentInParent<Transform>();

        float xMovement = Input.GetAxis("P" + playerNumber + "_Horizontal");
        float yMovement = Input.GetAxis("P" + playerNumber + "_Vertical");
        float jump = Input.GetAxis("P" + playerNumber + "_Jump");
        float punch = Input.GetAxis("P" + playerNumber + "_Punch");
        float kick = Input.GetAxis("P" + playerNumber + "_Kick");

        //Horizontal Changes
        if (xMovement != 0)
        {
            //vars
            Vector3 rotation = transform.localScale;
            Vector3 updatedHeading = rotation;
            float quotient = xMovement / Mathf.Abs(xMovement);
            //Rotation
            if (quotient != lastHeadings[playerNumber-1])
            {
                updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                lastHeadings[playerNumber - 1] = quotient;
            }
            //Horizontal Movement
            transform.position = new Vector3(transform.position.x + (lastHeadings[playerNumber -1] * player2.GetMoveSpeed()), transform.position.y, transform.position.z);
            transform.localScale = updatedHeading;
            player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.WALKING);
        }

        //Vertical Changes
        if (jump > 0)
        {
            //Vertical movement
            transform.position = new Vector3(transform.position.x, transform.position.y + (player2.GetMoveSpeed() * jump), transform.position.z);
            player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.JUMP);
        }


        /*Player Moves*/
        //Punch
        if(punch != 0)
        {
            player.GetCharacter().MovePunch();
            player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HIGHPUNCH);
        }
        //Kick

        //Special1

        //Special2

        //Ultra

    }
}

