using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Game : MonoBehaviour {

    public float maxRoundTime = 300;
    public float roundEndDelay = 3;
    public int rounds = 3;
    public int lives = 2;
    public int MaxPlayers = 2;
    private bool host = false;
    public GlobalGameData globalDataPrefab;

    private NetworkHandler network;
    private GameMode gameMode;
    private NetworkGameData networkGameData;
    private static Game instance;
    private static Player localPlayer;




    //Temp
    public GameObject playerPrefab;
    private Player localP1;
    private Player localP2;
    Transform spawnP1;
    Transform spawnP2;

   //State
    private State state = State.STARTING;
    private int currentRound = 0;
    private float roundStartTime = 0;
    private float countDownTimer = 0;
    private float roundEndTimer = 0;


    public void Awake()
    {
        //Network Test Object
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        if (!GlobalGameData.GetInstance()) Instantiate(globalDataPrefab);
        gameMode = GlobalGameData.GetInstance().selectedGameMode;
        instance = this;

        //Temp
    }

    public void Start()
    {
        state = State.STARTING;
    }
    /*************************************************************************/

    /// <summary>
    /// Notifies game of a player death - If not hosting Notifies host
    /// </summary>
    /// <param name="player"></param>
    public void TriggerDeath(Player player)
    {
        Debug.Log("Player " + player.name + " has died");
        state = State.ROUND_ENDING;
    }

    public void RegisterPlayer(Player player, NetworkIdentity id)
    {
        //Temp
        if (gameMode == GameMode.LOCALMULTIPLAYER) return;

        Debug.Log("Registering Player with game id = " + id.netId.ToString());
        if (networkGameData.networkPlayers.Count < MaxPlayers)
        {
            networkGameData.networkPlayers.Add(new NetworkGameData.PlayerRecord(id));
        }
    }

    public void UnregisterPlayer(Player player, NetworkIdentity id)
    {
        //Temp
        if (gameMode == GameMode.LOCALMULTIPLAYER) return;

        networkGameData.networkPlayers.Remove(new NetworkGameData.PlayerRecord(id));

    }

    public int GetLives()
    {
        return lives;
    }
    
    public int GetRounds()
    {
        return rounds;
    }

    public bool IsHost()
    {
        return host;
    }

    public NetworkHandler GetNetworkHandler()
    {
        return network;
    }

    public State GetState()
    {
        return state;
    }

    public GameMode GetGameMode()
    {
        return gameMode;
    }

    public float GetRemainingRoundTime()
    {
        return maxRoundTime - (Time.time - roundStartTime) ;
    }




    /*************************************************************************/
    /// <summary>
    /// Retrieves registered Players from the game. Provide a player number. Players
    /// are registered in order as they come. null if that player is unavailable
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns></returns>
    public Player GetPlayer(int playerNumber)
    {
        //Temp
        if(gameMode == GameMode.LOCALMULTIPLAYER)
        {
            switch (playerNumber)
            {
                case 0:
                    return localP1;
                case 1:
                    return localP2;
                default:
                    return null;

            }
        }


        if (playerNumber >= networkGameData.networkPlayers.Count) return null;
        GameObject player = ClientScene.FindLocalObject(networkGameData.networkPlayers[playerNumber].id.netId);
        if (!player) return null;
        Player result = player.GetComponent<Player>();
        return result;
    }

    public int GetNumberOfPlayers()
    {
        if (gameMode == GameMode.LOCALMULTIPLAYER) return 2;

        return networkGameData.networkPlayers.Count;
    }
    /// <summary>
    /// Used to get the local instance of a player
    /// </summary>
    /// <returns> Local Player -> 1 Per Client </returns>
    public static Player GetLocalPlayer()
    {
        return localPlayer;
    }

    /// <summary>
    /// Get's Game Singleton
    /// </summary>
    /// <returns> Single instance of the client game</returns>
    public static Game GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("Game was not properly instantiated");
        }
        //Debug.Log("Offering Game");
        return instance;
    }

    void Update()
    {
        /*foreach (NetworkGameData.PlayerRecord record in networkGameData.networkPlayers)
        {
            //Debug.Log("I have a player - " + record.id.netId.ToString());
        }
        */
        //TODO Updates based on inputs and notifications - Biggest being death notfication
        switch (state)
        {
            case State.PAUSED:
                //Need To Save Round Time
                break;

            case State.STARTING:
                Debug.Log("Now Starting a new Match!");
                currentRound = 0;
                spawnOpponents();
                //Make sure all players are loaded
                state = State.ROUND_BEGINING;
                break;

            case State.ROUND_BEGINING:
                currentRound++;
                state = State.FIGHTING;
                roundStartTime = Time.time;
                break;

            case State.FIGHTING:
                Debug.Log("Now Fighting");
                Debug.Log(roundStartTime + maxRoundTime + "start " + Time.time + "current");
                if(Time.time >= roundStartTime + maxRoundTime)
                {
                    //Begin Round End
                    
                    state = State.ROUND_ENDING;
                    roundEndTimer = Time.time;
                }
                break;

            case State.ROUND_ENDING:
                if (roundEndTimer + roundEndDelay >= Time.time) break;

                if(currentRound >= rounds)
                {
                    state = State.COMPLETED;
                } else
                {
                    state = State.ROUND_BEGINING;
                }
                break;

            case State.COMPLETED:
                //Process any needed network transactions
                state = State.SUMMARIZING;
                break;

            case State.SUMMARIZING:
                //Display Stats To Player and option to quit
                //Temp Send To Match begining
                break;
        }
    }



    private void spawnOpponents()
    {
        if(localP1 || localP2)
        {
            Destroy(localP1.GetComponent<GameObject>());
            Destroy(localP2.GetComponent<GameObject>());
        }
        spawnP1 = GameObject.FindGameObjectWithTag("P1Spawn").GetComponent<Transform>();
        spawnP2 = GameObject.FindGameObjectWithTag("P2Spawn").GetComponent<Transform>();
        if (gameMode == GameMode.LOCALMULTIPLAYER)
        {
            //Spawn Players 1 and 2
            GameObject p1 = Instantiate(playerPrefab, spawnP1.position, spawnP1.rotation);
            localP1 = p1.GetComponent<Player>();
            localP1.InitializeWithCharacter(Character.Get(GlobalGameData.GetInstance().player1Char));
            GameObject p2 = Instantiate(playerPrefab, spawnP2.position, spawnP2.rotation);
            localP2 = p2.GetComponent<Player>();
            localP2.InitializeWithCharacter(Character.Get(GlobalGameData.GetInstance().player2Char));
            Vector3 p2LS = p2.GetComponentInParent<Transform>().localScale;
            p2.GetComponentInParent<Transform>().localScale = new Vector3(-1 * p2LS.x, p2LS.y, p2LS.z);

        }
    }


    private void respawnOpponents()
    {
        localP1.GetComponent<Transform>().position = spawnP1.position;
        localP2.GetComponent<Transform>().position = spawnP2.position;
        Vector3 p2LS = localP2.GetComponentInParent<Transform>().localScale;
        localP2.GetComponentInParent<Transform>().localScale = new Vector3(-1 * p2LS.x, p2LS.y, p2LS.z);
    }
    /*************************************************************************/
    public enum State
    {
        PAUSED,
        STARTING,
        ROUND_BEGINING,
        FIGHTING,
        ROUND_ENDING,
        SUMMARIZING,
        COMPLETED
    }

    public enum GameMode
    {
        SINGLEPLAYER,
        LOCALMULTIPLAYER,
        NETWORKMULTIPLAYER
    }

    


}
