using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    #region Private Fields
    private Animator animator;
    [SerializeField]
    private float directionDampTime = 0.25f;
    #endregion

    #region MonoBehaviour CallBacks


    public void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Player animator is missing!!!!", this);
        }
    }

    public void Update()
    {
        if (!animator) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if(stateInfo.IsName("Base Layer.Run"))
        {
            if (GamePad.GetState(PlayerIndex.One).Released(CButton.X))
            {
                animator.SetTrigger("Jump");
            }
        }

        float h = GamePad.GetAxis(CAxis.LX, PlayerIndex.One);
        float v = GamePad.GetAxis(CAxis.LY, PlayerIndex.One);

        if (v < 0)
        {
            v = 0;
        }

        animator.SetFloat("Speed", h * h + v);

        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }


    #endregion

}



