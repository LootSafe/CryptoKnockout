using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAnimationControl : MonoBehaviour {

    SuperStates state;
    public float postLenth;
    public float midLength;
    public float introLength;
    public GameObject AnimationObject;

    private PlayerAnimatorController PAC;

    private float midTime;
    private float postTime;
    private float endTime;
	// Use this for initialization
	void Start () {
        PAC = GetComponent<PlayerAnimatorController>();
        midTime = Time.time + introLength;
        postTime = midTime + midLength;
        endTime = postTime + postLenth;

	}

	// Update is called once per frame
	void Update () {

        switch (state) {
            case SuperStates.INTRO:
                UpdateIntro();
                break;
            case SuperStates.MID:
                UpdateMid();
                break;
            case SuperStates.POST:
                UpdatePost();
                break;
            case SuperStates.END:
                UpdateEnd();
                break;
            default:
                break;
        }

	}

    public virtual void UpdateIntro()
    {
        if(Time.time >= midTime)
        {
            CompleteSequence();
        }
    }

    public virtual void UpdateMid()
    {
        if(Time.time >= postTime)
        {
            CompleteSequence();
        }
    }

    public virtual void UpdatePost()
    {
        if (Time.time >= endTime)
        {
            CompleteSequence();
        }
    }

    public virtual void UpdateEnd()
    {
        state = SuperStates.WAITING;
    }

    public void CompleteSequence()
    {
        switch (state)
        {
            case SuperStates.INTRO:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.SUPER);
                state = SuperStates.MID;
                break;
            case SuperStates.MID:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.MIDSUPER);
                state = SuperStates.POST;
                break;
            case SuperStates.POST:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.POSTSUPER);
                state = SuperStates.END;
                break;
            case SuperStates.END:
                state = SuperStates.WAITING;
                break;
            default:
                break;
        }
    }
}
