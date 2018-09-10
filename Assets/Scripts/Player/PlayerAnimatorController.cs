using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    public bool DEBUG = false; // REMOVE ME LATER

    bool IDLE = false;

    public enum ANIMATION_STATE {IDLE, BLOCK, DEAD, JUMP, HURT, LOWPUNCH, LOWKICK, HIGHPUNCH, HIGHKICK, SPECIALATTACKONE, DUCK };


    Player player;
    /* Animator Object */

    Animator playerAnimator;

    /* State Vars */

    ANIMATION_STATE currentAnimationState;
    Rigidbody2D rgbody;

    void Update()
    {
        playerAnimator.SetFloat("SPEED", Mathf.Abs(rgbody.velocity.x));
        playerAnimator.SetBool("GROUNDED", player.IsGrounded());
        playerAnimator.SetBool("ALIVE", player.IsAlive());
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
                    playerAnimator.SetTrigger("DIE");
                    return;
            case ANIMATION_STATE.DUCK:
                    playerAnimator.SetTrigger("DUCK");
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
                    playerAnimator.SetTrigger("BLOCKING");
                    return;
                case ANIMATION_STATE.LOWPUNCH:
                    return;
                case ANIMATION_STATE.LOWKICK:
                    return;
                case ANIMATION_STATE.HIGHPUNCH:
                    playerAnimator.SetTrigger("PUNCHING");
                    return;
                case ANIMATION_STATE.HIGHKICK:
                    playerAnimator.SetTrigger("KICKING");
                    return;
                case ANIMATION_STATE.SPECIALATTACKONE:
                    playerAnimator.SetTrigger("SPECIAL");
                    return;
                default:
                    playerAnimator.SetTrigger("GROUNDED");
                    return;
            }
       
    }


    public void UpdateController()
    {
        playerAnimator = GetComponent<Animator>();
    }

}
