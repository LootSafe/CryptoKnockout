using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    public bool DEBUG = false; // REMOVE ME LATER

    bool IDLE = false;

    public enum GROUNDED_STATE { GROUNDED, NOTGROUNDED };
    public enum ANIMATION_STATE { ALIVE, IDLE, BLOCK, DEAD, JUMP, HURT, LOWPUNCH, LOWKICK, HIGHPUNCH, HIGHKICK, SPECIALATTACKONE };


    Player player;
    /* Animator Object */

    Animator playerAnimator;

    /* State Vars */

    GROUNDED_STATE groundedState;
    ANIMATION_STATE currentAnimationState;
    Rigidbody2D rgbody;

    void Update()
    {
        playerAnimator.SetFloat("speed", Mathf.Abs(rgbody.velocity.x));
    }
    void Start()
    {
        UpdateController();
        SetAnimationState(ANIMATION_STATE.IDLE);
        player = GetComponent<Player>();
        rgbody = GetComponent<Rigidbody2D>();
    }

    /* Animation State */

    public ANIMATION_STATE GetCurrentAnimationState()
    {
        return currentAnimationState;
    }

    public void SetAnimationState(ANIMATION_STATE animationState)
    {
            switch (animationState)
            {
                case ANIMATION_STATE.DEAD:
                    playerAnimator.SetBool("DEAD", true);
                    return;
                case ANIMATION_STATE.ALIVE:
                    playerAnimator.SetBool("DEAD", false);
                    Debug.Log("I'm coming back to life");
                    return;
                case ANIMATION_STATE.IDLE:
                    playerAnimator.SetTrigger("GROUNDED");
                     return;
                case ANIMATION_STATE.JUMP:
                    playerAnimator.SetTrigger("JUMP");
                    return;
                case ANIMATION_STATE.HURT:
                    playerAnimator.SetTrigger("HURT");
                    return;
                case ANIMATION_STATE.BLOCK:
                    return;
                case ANIMATION_STATE.LOWPUNCH:
                    return;
                case ANIMATION_STATE.LOWKICK:
                    return;
                case ANIMATION_STATE.HIGHPUNCH:
                    playerAnimator.SetTrigger("PUNCHING");
                    return;
                case ANIMATION_STATE.HIGHKICK:
                    return;
                case ANIMATION_STATE.SPECIALATTACKONE:
                    return;
                default:
                    playerAnimator.SetTrigger("GROUNDED");
                    return;
            }
       
    }

    public void SetGroundedState(bool isGrounded)
    {
        if (isGrounded)
        {
            groundedState = GROUNDED_STATE.GROUNDED;
        }
        else
        {
            groundedState = GROUNDED_STATE.NOTGROUNDED;
        }
    }

    public GROUNDED_STATE GetGroundedState(bool isGrounded)
    {
        return groundedState;
    }

    public void UpdateController()
    {
        playerAnimator = GetComponent<Animator>();
    }

}
