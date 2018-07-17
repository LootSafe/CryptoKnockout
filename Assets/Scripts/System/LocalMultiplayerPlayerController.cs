using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerPlayerController : MonoBehaviour {

    float[] lastHeadings;
    bool[,] controlLocks;
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


        //Initialize Control Locks
        controlLocks = new bool[8, 4];
	}

    // Update is called once per frame
    void FixedUpdate() {

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

        UpdatePlayer(player1, 1);
        UpdatePlayer(player2, 2);
    }

    private void UpdatePlayer(Player player, int playerNumber)
    {
        if (!player) return;
        if (game.GetState() != Game.State.FIGHTING) return;

        Transform transform = player.GetComponentInParent<Transform>();
        Rigidbody2D rigidbody = player.GetComponentInParent<Rigidbody2D>();

        float xMovement = Input.GetAxis("P" + playerNumber + "_Horizontal");
        float yMovement = Input.GetAxis("P" + playerNumber + "_Vertical");
        //0
        float jump = Input.GetAxis("P" + playerNumber + "_Jump");
        //1
        float punch = Input.GetAxis("P" + playerNumber + "_Punch");
        //2
        float kick = Input.GetAxis("P" + playerNumber + "_Kick");
        //3
        float block = Input.GetAxis("P" + playerNumber + "_Block");

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
            //transform.position = new Vector3(transform.position.x + (lastHeadings[playerNumber -1] * player.GetMoveSpeed()), transform.position.y, transform.position.z);
            rigidbody.AddForce(new Vector2(xMovement * player.GetMoveSpeed(), 0));
            transform.localScale = updatedHeading;
            player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.WALKING);
        }
        else
        {
            player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.IDLE);
        }

        //Vertical Changes
        if (jump != 0)
        {
            if (controlLocks[playerNumber - 1, 0] == false)
            {
                player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.JUMP);
                rigidbody.AddForce(new Vector2(0, 350));
                controlLocks[playerNumber - 1, 0] = true;
            }
        }
        else
        {
            if (player.IsGrounded())
            {
                controlLocks[playerNumber - 1, 0] = false;
            }
        }


        /*Player Moves*/
        //Punch

        if (punch != 0 )
        {
            if (controlLocks[playerNumber - 1, 1] == false)
            {
                player.GetCharacter().MovePunch();
                player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HIGHPUNCH);
                controlLocks[playerNumber - 1, 1] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 1] = false;
        }
        
        
        //Kick
        if (kick != 0)
        {
            if (controlLocks[playerNumber - 1, 2] == false)
            {
                player.GetCharacter().MoveKick();
                player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.LOWKICK);
                controlLocks[playerNumber - 1, 2] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 2] = false;
        }

        //Block
        if (block != 0)
        {
            if (controlLocks[playerNumber - 1, 3] == false)
            {
                player.GetCharacter().MoveBlock();
                player.GetComponent<PlayerAnimatorController>().SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.BLOCK);
                controlLocks[playerNumber - 1, 3] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 3] = false;
        }

        //Special2

        //Ultra

    }
}

