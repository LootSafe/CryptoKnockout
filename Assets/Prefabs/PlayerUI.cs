﻿using UnityEngine;
using UnityEngine.UI;


using System.Collections;

public class PlayerUI : MonoBehaviour
{
    LaserPlayerAnimator target;

    #region Private Fields


    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText;


    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Slider playerHealthSlider;

    [Tooltip("Pixel offset from the player target")]
    [SerializeField]
    private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

    float characterControllerHeight = 0f;
    Transform targetTransform;
    Vector3 targetPosition;


    #endregion


    #region MonoBehaviour CallBacks

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        // Reflect the Player Health
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = target.Health;
        }

        LateUpdate();

    }
    #endregion


    #region Public Methods

    public void SetTarget(LaserPlayerAnimator _target)
    {

        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        // Cache references for efficiency
        target = _target;
        if (playerNameText != null)
        {
            Debug.Log("YEP");
            playerNameText.text = target.photonView.Owner.NickName;
        }


        CharacterController _characterController = _target.GetComponent<CharacterController>();
        // Get data from the Player that won't change during the lifetime of this Component
        if (_characterController != null)
        {
            characterControllerHeight = _characterController.height;
        }

    }

    void LateUpdate()
    {
        // #Critical
        // Follow the Target GameObject on screen.
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }
    
    #endregion


}
