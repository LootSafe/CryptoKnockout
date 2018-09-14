using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerPlayerController : MonoBehaviour {

    float[] lastHeadings;
    bool[,] controlLocks;
    float[] lastMovements;

    bool jumpLock;

    bool pauseMenuLock = false;

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
        controlLocks = new bool[8, 7];
        lastMovements = new float[8];

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

        //Escape Menu
        if (GamePad.GetButton(CButton.Start)){
            if (!pauseMenuLock)
            {
                if(game.GetState() == Game.State.PAUSED)
                {
                    game.UnPause();
                }
                else
                {
                    game.Pause();
                }
                pauseMenuLock = true;
            }
        } else
        {
            pauseMenuLock = false;
        }

        UpdatePlayer(player1, 1);
        UpdatePlayer(player2, 2);
    }

    private void UpdatePlayer(Player player, int playerNumber)
    {

        PlayerIndex pi;
        switch (playerNumber)
        {
            case 1:
                pi = PlayerIndex.One;
                break;
            case 2:
                pi = PlayerIndex.Two;
                break;
            case 3:
                pi = PlayerIndex.Three;
                break;
            case 4:
                pi = PlayerIndex.Four;
                break;
            case 5:
                pi = PlayerIndex.Five;
                break;
            case 6:
                pi = PlayerIndex.Six;
                break;
            case 7:
                pi = PlayerIndex.Seven;
                break;
            case 8:
                pi = PlayerIndex.Eight;
                break;
            default:
                pi = PlayerIndex.Any;
                break;
        }

        if (!player) return;
        if (game.GetState() != Game.State.FIGHTING) return;

        Transform transform = player.GetComponentInParent<Transform>();
        Rigidbody2D rigidbody = player.GetComponentInParent<Rigidbody2D>();
        PlayerAnimatorController pac = player.GetComponent<PlayerAnimatorController>();


        //float xMovement = Input.GetAxisRaw("P" + playerNumber + "_Horizontal");
        float xMovement = GamePad.GetAxis(CAxis.LX, pi);

        //4 -- For Crouch
        //float yMovement = Input.GetAxisRaw("P" + playerNumber + "_Vertical");
        float yMovement = GamePad.GetAxis(CAxis.LY, pi);
        //0
        //Deprecated
        float jump = Input.GetAxis("P" + playerNumber + "_Jump");
        //1
        //float punch = Input.GetAxis("P" + playerNumber + "_Punch");
        bool punch = GamePad.GetButton(CButton.A, pi);
        //2
        //float kick = Input.GetAxis("P" + playerNumber + "_Kick");
        bool kick = GamePad.GetButton(CButton.B, pi);
        //3
        //float block = Input.GetAxis("P" + playerNumber + "_Block");
        bool block = GamePad.GetButton(CButton.X, pi);
        //5
        //float super = Input.GetAxis("P" + playerNumber + "_Super");
        bool super = GamePad.GetButton(CButton.Y, pi);
        //6 Jump Lock


        //Character Lock
        if (player.IsBlocking() || player.IsAttacking() || player.IsDucking())
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }



        //Horizontal Changes
        if (xMovement != 0 && player.IsAlive() && !player.IsAttacking() && !player.IsDucking() && !player.IsDucking())
        {
            if (Time.time - lastMovements[playerNumber - 1] >= 0.01) 
            {
                //vars
                Vector3 rotation = transform.localScale;
                Vector3 updatedHeading = rotation;
                float quotient = xMovement / Mathf.Abs(xMovement);
                //Heading Flip
                if (quotient != lastHeadings[playerNumber - 1])
                {
                    updatedHeading = new Vector3(rotation.x * -1, rotation.y, rotation.z);
                    rigidbody.velocity = new Vector2(-1 * rigidbody.velocity.x/2, rigidbody.velocity.y);
                    lastHeadings[playerNumber - 1] = quotient;
                }
                //Horizontal Movement
                //transform.position = new Vector3(transform.position.x + (lastHeadings[playerNumber -1] * player.GetMoveSpeed()), transform.position.y, transform.position.z);
                rigidbody.AddForce(new Vector2(xMovement * player.GetMoveSpeed(), 0));
                transform.localScale = updatedHeading;
                lastMovements[playerNumber - 1] = Time.time;
            }
        }
        else
        {
            pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.IDLE);
        }

        //Vertical Changes
        if (yMovement != 0 && !player.IsAttacking() || controlLocks[playerNumber - 1, 6])
        {
            if (Mathf.RoundToInt(yMovement) > 0 || controlLocks[playerNumber - 1, 6])
            {
                if (controlLocks[playerNumber - 1, 0] == false)
                {
                    if (!controlLocks[playerNumber - 1, 6])
                    {
                        pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.JUMP);
                        controlLocks[playerNumber - 1, 6] = true;
                    }
                    else
                    {
                        rigidbody.AddForce(new Vector2(0, 400));
                        controlLocks[playerNumber - 1, 0] = true;
                        controlLocks[playerNumber - 1, 6] = false;
                    }
                    

                }
                else
                {

                }
            }
            else if(Mathf.RoundToInt(yMovement) < 0)
            {
                if (controlLocks[playerNumber - 1, 4] == false && player.IsGrounded())
                {
                    controlLocks[playerNumber - 1, 4] = true;
                    pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.DUCK);
                    player.StartDucking();
                }
            }
            else
            {
                pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.IDLE);
                
            }
        }
        else
        {
            if (player.IsGrounded())
            {
                controlLocks[playerNumber - 1, 0] = false;
            }
            controlLocks[playerNumber - 1, 4] = false;
            player.StopDucking();
        }


        /*Player Moves*/
        //Punch

        if (punch)
        {
            if (controlLocks[playerNumber - 1, 1] == false && !player.IsHurt() && !player.IsAttacking())
            {
                player.GetCharacter().MovePunch();
                pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HIGHPUNCH);
                controlLocks[playerNumber - 1, 1] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 1] = false;
        }
        
        
        //Kick
        if (kick)
        {
            if (controlLocks[playerNumber - 1, 2] == false && !player.IsHurt() && !player.IsAttacking())
            {
                player.GetCharacter().MoveKick();
                pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.HIGHKICK);
                controlLocks[playerNumber - 1, 2] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 2] = false;
        }

        //Block
        if (block)
        {
            if (controlLocks[playerNumber - 1, 3] == false && player.IsGrounded())
            {
                    player.GetCharacter().MoveBlock();
                    pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.BLOCK);
                    controlLocks[playerNumber - 1, 3] = true;
                    player.StartBlocking();
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 3] = false;
            pac.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.IDLE);
            player.StopBlocking();
        }

        //Special2

        //Ultra
        if (super)
        {
            if (controlLocks[playerNumber - 1, 5] == false && !player.IsHurt() && !player.IsAttacking())
            {
                player.GetCharacter().MoveUltra();
                controlLocks[playerNumber - 1, 5] = true;
            }
        }
        else
        {
            controlLocks[playerNumber - 1, 5] = false;
        }

    }
}

