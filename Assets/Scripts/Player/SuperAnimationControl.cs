using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAnimationControl : MonoBehaviour {

    public SuperStates state;
    public float postLenth;
    public float midLength;
    public float introLength;
    public float waitLength = 1f;
    public GameObject animationObject;

    private PlayerAnimatorController PAC;
    protected Player player;

    protected float midTime;
    protected float postTime;
    protected float endTime;
    protected float waitTime;

    protected Player opponent;
    protected Game game;

	// Use this for initialization
	public virtual void Start () {
        player = GetComponent<Player>();
        PAC = GetComponent<PlayerAnimatorController>();
        midTime = Time.time + introLength;
        postTime = midTime + midLength;
        endTime = postTime + postLenth;
        waitTime = endTime + waitLength;
        state = SuperStates.WAITING;

	}

	// Update is called once per frame
	public virtual void Update () {
        if(!game)
        {
            game = Game.GetInstance();
            return;
        }

        if (!opponent)
        {
            opponent = game.GetOpponent(player.GetPlayerNumber());
        }

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
            player.NotifySuperComplete();
            NextSequence();
        }
    }

    public virtual void UpdateEnd()
    {
        if (Time.time >= waitTime)
        {
            NextSequence();
        }
    }

    public virtual void StartSequence()
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
                PAC.SetAnimationState(PlayerAnimatorController.ANIMATION_STATE.ENDSUPER);
                this.state = SuperStates.END;
                break;
            case SuperStates.END:
                Debug.Log("Resetting Super and Notifying Player");
                this.state = SuperStates.WAITING;
                break;
            default:
                break;
        }
    }
}
