using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// Player manager.
/// Handles fire Input and Beams.
/// </summary>
public class LaserPlayerAnimator : MonoBehaviourPunCallbacks
{

    #region Private Fields
    [Tooltip("The current Health of Our Player")]
    public float Health = 1f;

    [Tooltip("The Beams GameObject to control")]
    [SerializeField]
    private GameObject beams;
    //True, when the user is firing
    bool IsFiring;
    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        if (beams == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
        }
        else
        {
            beams.SetActive(false);
        }
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// </summary>
    void Update()
    {
        ProcessInputs();
        // trigger Beams active state
        if (beams != null && IsFiring != beams.activeSelf)
        {
            beams.SetActive(IsFiring);
        }

        if (Health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!Other.name.Contains("Beam"))
        {
            return;
        }

        Health -= 0.1f;
    }

    void OnTiggerStay(Collider Other)
    {
        if(!photonView.IsMine)
        {
            return;
        }

        if(! photonView.IsMine)
        {
            return;
        }

        Health -= 0.1f * Time.deltaTime;
    }

    #endregion

    #region Custom

    /// <summary>
    /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
    /// </summary>
    void ProcessInputs()
    {
        if (GamePad.GetState().Released(CButton.B))
        {
            Debug.Log("Lasers Fired");
            if (!IsFiring)
            {
                IsFiring = true;
            }
            else
            {
                IsFiring = false;
            }
        }
    }

    #endregion
}
