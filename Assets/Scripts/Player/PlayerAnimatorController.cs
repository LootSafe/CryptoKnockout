using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    public bool DEBUG = false; // REMOVE ME LATER

    bool IDLE = false;

    public enum GROUNDED_STATE { GROUNDED, NOTGROUNDED };
    public enum DEAD_STATE { DEAD, ALIVE };
    public enum ANIMATION_STATE { ALIVE, IDLE, WALKING, RUNNING, BLOCK, DEAD, JUMP, HURT, LOWPUNCH, LOWKICK, HIGHPUNCH, HIGHKICK, SPECIALATTACKONE };

    /* Animator Object */

    Animator playerAnimator;

    /* State Vars */

    GROUNDED_STATE groundedState;
    ANIMATION_STATE currentAnimationState;
    DEAD_STATE deadState;
    Rigidbody2D rgbody;

    void Update()
    {
        playerAnimator.SetFloat("speed", Mathf.Abs(rgbody.velocity.x));
    }
    void Start()
    {
        UpdateController();
        deadState = DEAD_STATE.ALIVE;
        SetAnimationState(ANIMATION_STATE.IDLE);

        rgbody = GetComponent<Rigidbody2D>();
    }

    /* Animation State */

    public ANIMATION_STATE GetCurrentAnimationState()
    {
        return currentAnimationState;
    }

    public bool SetAnimationState(ANIMATION_STATE animationState)
    {
        if (deadState == DEAD_STATE.ALIVE)
        {
            switch (animationState)
            {
                case ANIMATION_STATE.DEAD:
                    playerAnimator.SetBool("DEAD", true);
                    return true;
                case ANIMATION_STATE.ALIVE:
                    playerAnimator.SetBool("DEAD", false);
                    Debug.Log("I'm coming back to life");
                    return true;
                case ANIMATION_STATE.IDLE:
                    playerAnimator.SetTrigger("GROUNDED");
                    playerAnimator.SetBool("WALKING", false);
                    return true;
                case ANIMATION_STATE.WALKING:
                    playerAnimator.SetBool("WALKING", true);
                    return true;
                case ANIMATION_STATE.RUNNING:
                    playerAnimator.SetBool("RUNNING", true);
                    playerAnimator.SetTrigger("RUNNING");
                    return true;
                case ANIMATION_STATE.JUMP:
                    playerAnimator.SetTrigger("JUMP");
                    return true;
                case ANIMATION_STATE.HURT:
                    playerAnimator.SetTrigger("HURT");
                    return true;
                case ANIMATION_STATE.BLOCK:
                    return true;
                case ANIMATION_STATE.LOWPUNCH:
                    return true;
                case ANIMATION_STATE.LOWKICK:
                    return true;
                case ANIMATION_STATE.HIGHPUNCH:
                    playerAnimator.SetTrigger("PUNCHING");
                    return true;
                case ANIMATION_STATE.HIGHKICK:
                    return true;
                case ANIMATION_STATE.SPECIALATTACKONE:
                    return true;
                default:
                    playerAnimator.SetTrigger("GROUNDED");
                    return true;
            }
        }
        else
        {
            return false;
        }
    }

    public DEAD_STATE GetDeadState()
    {
        return deadState;
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
