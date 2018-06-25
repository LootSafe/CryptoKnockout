using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

	public enum GROUNDED_STATE { GROUNDED, NOTGROUNDED};
    public enum ANIMATION_STATE { IDLE, BLOCK, DEAD, JUMP, HURT, LOWPUNCH, LOWKICK, HIGHPUNCH, HIGHKICK, SPECIALATTACKONE };

    /* Animator Object */

    Animator playerAnimator;

    /* State Vars */

    GROUNDED_STATE groundedState;
    ANIMATION_STATE currentAnimationState;

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
        switch(animationState)
        {
            case ANIMATION_STATE.IDLE:
                return true;
            case ANIMATION_STATE.BLOCK:
                return true;
            case ANIMATION_STATE.DEAD:
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
        }

        return false;
    }

    /* State Helpers */

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
