using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
/// <summary>
/// Player manager.
/// Handles fire Input and Beams.
/// </summary>
public class LaserPlayerAnimator : MonoBehaviourPunCallbacks, IPunObservable
{

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;


    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;
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
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();


        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }

        #if UNITY_5_4_OR_NEWER
                // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
                {
                    this.CalledOnLevelWasLoaded(scene.buildIndex);
                };
        #endif

        if (playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);

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
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
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


        #if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif


    void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }

        GameObject _uiGo = Instantiate(this.playerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
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

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(IsFiring);
            stream.SendNext(Health);
        }
        else
        {
            this.IsFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }


    #endregion
}
