using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAnimationControl : MonoBehaviour {

    public SuperStates state;
    public float postLenth;
    public float midLength;
    public float introLength;
    public GameObject AnimationObject;

    private PlayerAnimatorController PAC;
    private Player player;

    private float midTime;
    private float postTime;
    private float endTime;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        PAC = GetComponent<PlayerAnimatorController>();
        midTime = Time.time + introLength;
        postTime = midTime + midLength;
        endTime = postTime + postLenth;
        state = SuperStates.WAITING;

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
            NextSequence();
        }
    }

    public virtual void UpdateMid()
    {
        if(Time.time >= postTime)
        {
            NextSequence();
        }
    }

    public virtual void UpdatePost()
    {
        if (Time.time >= endTime)
        {
            NextSequence();
        }
    }

    public virtual void UpdateEnd()
    {
        NextSequence();
    }

    public void StartSequence()
    {
        Debug.Log("Sequence Initiated...");
        Start();
        NextSequence(SuperStates.WAITING);

    }

    public void NextSequence()
    {
        Debug.Log("Trying to Update State");
        NextSequence(this.state);
    }

    public void NextSequence(SuperStates state)
    {
        switch (state)
        {
            case SuperStates.WAITING:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.SUPER);
                Debug.Log("Starting Super");
                this.state = SuperStates.INTRO;
                break;
            case SuperStates.INTRO:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.MIDSUPER);
                Debug.Log("Holding States");
                this.state = SuperStates.MID;
                break;
            case SuperStates.MID:
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.POSTSUPER);
                Debug.Log("Ending Sper");
                this.state = SuperStates.POST;
                break;
            case SuperStates.POST:
                Debug.Log("Super Has Ended");
                this.state = SuperStates.END;
                break;
            case SuperStates.END:
                Debug.Log("Resetting Super and Notifying Player");
                player.NotifySuperComplete();
                this.state = SuperStates.WAITING;
                break;
            default:
                break;
        }
    }
}
