using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

	public enum GROUNDED_STATE { GROUNDED, NOTGROUNDED};
    public enum DEAD_STATE { DEAD, ALIVE };
    public enum ANIMATION_STATE { IDLE, WALKING, RUNNING, BLOCK, DEAD, JUMP, HURT, LOWPUNCH, LOWKICK, HIGHPUNCH, HIGHKICK, SPECIALATTACKONE };

    /* Animator Object */

    Animator playerAnimator;

    /* State Vars */

    GROUNDED_STATE groundedState;
    ANIMATION_STATE currentAnimationState;
    DEAD_STATE deadState;

    /* Basic Methods */

    public void Start()
    {
        playerAnimator.GetComponent<Animator>();
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
                case ANIMATION_STATE.IDLE:
                    return true;
                case ANIMATION_STATE.WALKING:
                    return true;
                case ANIMATION_STATE.RUNNING:
                    return true;
                case ANIMATION_STATE.BLOCK:
                    return true;
                case ANIMATION_STATE.JUMP:
                    return true;
                case ANIMATION_STATE.HURT:
                    return true;
                case ANIMATION_STATE.LOWPUNCH:
                    return true;
                case ANIMATION_STATE.LOWKICK:
                    return true;
                case ANIMATION_STATE.HIGHPUNCH:
                    return true;
                case ANIMATION_STATE.HIGHKICK:
                    return true;
                case ANIMATION_STATE.SPECIALATTACKONE:
                    return true;
                default:
                    return true;
            }
        }
        else
        {
            return false;
        }
    }

    /* State Helpers */

    public void SetDeadState(bool isDead)
    {
        if (isDead)
        {
            playerAnimator.SetBool("DEAD", true);
            currentAnimationState = ANIMATION_STATE.DEAD;
            deadState = DEAD_STATE.DEAD;
        }
        else
        {
            playerAnimator.SetBool("DEAD", false);
            currentAnimationState = ANIMATION_STATE.IDLE;
            deadState = DEAD_STATE.ALIVE;
        }
    }

    public  DEAD_STATE GetDeadState()
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

}
