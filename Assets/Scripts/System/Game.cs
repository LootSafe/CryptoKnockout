using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Game : MonoBehaviour {

    public float maxRoundTime = 120;
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

    //Pausing
    private float pauseTime;
    private State lastState;

   //State
    private State state = State.STARTING;
    private int currentRound = 0;
    private float countDownTimer = 0;

    //Super
    private Player superUser;
    private bool superUsed;

    //Escape Menu
    public GameObject escapeMenu;
    GameLoader gl;

    //Streak System
    public float streakTime = 0.3f;

    private bool respawnToggle = false;
    private float roundStartTime = 0;
    private float roundEndTimer = 0;


    public void Awake()
    {
        //Network Test Object
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        if (!GlobalGameData.GetInstance()) Instantiate(globalDataPrefab);
        gameMode = GlobalGameData.GetInstance().selectedGameMode;
        instance = this;
        gl = GameObject.FindGameObjectWithTag("GameLoader").GetComponent<GameLoader>();
        //Temp
    }

    public void Start()
    {
        state = State.STARTING;
        escapeMenu = gl.escapeMenu;
        
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
        roundEndTimer = Time.time;
    }

    public bool TriggerSuper(Player player)
    {
        if (!superUsed)
        {
            Debug.Log("Player " + player.name + " is using his super ability");
            state = State.SUPER;
            superUsed = true;
            superUser = player;
            return true;
        }
        superUsed = false;
        return false;
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
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public bool IsHost()
    {
        return host;
    }

    public bool hasPlayers()
    {
        return GetPlayers().Length <= 0 ? false : true;
    }

    public bool hasAllPlayers()
    {
        return GetPlayers().Length > 1 ? true : false;
    }

    public int GetPlayerNumber(Player p)
    {
        

        return 0;
    }

    public Player GetOpponent(int i)
    {
        return i == 0 ? GetPlayer(1) : GetPlayer(0);
    }
    public void Pause()
    {
        if (gameMode == GameMode.LOCALMULTIPLAYER)
        {
            lastState = state;
            state = State.PAUSED;
            pauseTime = Time.time;
            escapeMenu.SetActive(true);
        }
    }

    public void UnPause()
    {
        if(gameMode == GameMode.LOCALMULTIPLAYER)
        {
            state = lastState;
            roundStartTime = roundStartTime + (Time.time - pauseTime);
            escapeMenu.SetActive(false);
        }
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
        float remainingTime = maxRoundTime - (Time.time - roundStartTime);

        if(state == State.FIGHTING || state == State.ROUND_ENDING)
        {
            return remainingTime;
        } else
        {
            if (remainingTime <= 0) return 0;
            return maxRoundTime;
        }
    }

    public float GetRemainingCountDownTime()
    {
        return 4 - (Time.time - countDownTimer);
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

    public Player[] GetPlayers()
    {
        Player[] p;
        if (gameMode == GameMode.LOCALMULTIPLAYER)
        {
            p = new Player[2];
            p[0] = localP1;
            p[1] = localP2;
        }
        else
        {
            GameObject tempPlayer;
            int i = 0;
            p = new Player[networkGameData.networkPlayers.Count];
            foreach(NetworkGameData.PlayerRecord playerRecord in networkGameData.networkPlayers)
            {
                tempPlayer = ClientScene.FindLocalObject(playerRecord.id.netId);
                p[i] = tempPlayer.GetComponent<Player>();
                i++;

            }
        }
        return p;
        
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
                countDownTimer = Time.time;
                break;

            case State.ROUND_BEGINING:
                if (!respawnToggle)
                {
                    respawnToggle = true;
                    respawnOpponents();
                }
                respawnToggle = false;
                if (GetRemainingCountDownTime() > 0) break;
                currentRound++;
                state = State.FIGHTING;
                roundStartTime = Time.time;
                break;

            case State.FIGHTING:
                if(Time.time >= roundStartTime + maxRoundTime)
                {
                    //Begin Round End
                    
                    state = State.ROUND_ENDING;
                    roundEndTimer = Time.time;
                }
                break;
            case State.SUPER:
                GetOpponent(superUser.GetPlayerNumber()).TakeDamage(99999999, superUser);
                GetOpponent(superUser.GetPlayerNumber()).notifyDeath();

                state = State.ROUND_ENDING;
                break;

            case State.ROUND_ENDING:
                if (Time.time - roundEndTimer < roundEndDelay) break;

                if(currentRound >= rounds)
                {
                    state = State.COMPLETED;
                } else
                {
                    superUsed = false;
                    superUser = null;
                    respawnToggle = false;
                    state = State.ROUND_BEGINING;
                    countDownTimer = Time.time;
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
            Debug.Log("Spawning ---");
            //Spawn Players 1 and 2
            GameObject p1 = Instantiate(CharacterSwapper.GetCharacter(GlobalGameData.GetInstance().player1Char), spawnP1.position, spawnP1.rotation);
            localP1 = p1.GetComponentInChildren<Player>();
            localP1.InitializeWithCharacter(Character.Get(GlobalGameData.GetInstance().player1Char));
            localP1.SetPlayerNumber(0);

            GameObject p2 = Instantiate(CharacterSwapper.GetCharacter(GlobalGameData.GetInstance().player2Char), spawnP2.position, spawnP2.rotation);
            localP2 = p2.GetComponentInChildren<Player>();
            localP2.InitializeWithCharacter(Character.Get(GlobalGameData.GetInstance().player2Char));
            localP2.SetPlayerNumber(1);

            Vector3 p2LS = localP1.GetComponent<Transform>().localScale;
            localP2.GetComponent<Transform>().localScale = new Vector3(-1 * Mathf.Abs(p2LS.x), p2LS.y, p2LS.z);

        }
    }


    private void respawnOpponents()
    {

        localP1.GetComponent<Transform>().position = spawnP1.position;
        localP2.GetComponent<Transform>().position = spawnP2.position;
        Vector3 p2LS = localP2.transform.localScale;
        localP2.transform.localScale = new Vector3(-1 * Mathf.Abs(p2LS.x), p2LS.y, p2LS.z);

        localP1.respawn();
        localP2.respawn();
    }

    /*************************************************************************/
    public enum State
    {
        PAUSED,
        STARTING,
        ROUND_BEGINING,
        FIGHTING,
        SUPER,
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
