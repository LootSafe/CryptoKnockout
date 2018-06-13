using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    
    public int rounds = 3;
    public int lives = 2;
    private bool host = true;

    private NetworkHandler network;
    private static Game instance;
    private static Player localPlayer;
    private State state = State.STARTING;
    


    void Start()
    {
        network = new NetworkHandler();
        instance = this;
        localPlayer = new Player(new TestCharacter());

    }
    /*************************************************************************/

    public void TriggerDeath(Player player)
    {
        Debug.Log("Player " + player.name + " has died");
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


    /*************************************************************************/

    /// <summary>
    /// Used to get the local instance of a player
    /// </summary>
    /// <returns> Local Player -> 1 Per Client </returns>
    public static Player GetPlayer()
    {
        return localPlayer;
    }

    /// <summary>
    /// Get's Game Singleton
    /// </summary>
    /// <returns> Single instance of the client game</returns>
    public static Game GetInstance()
    {
        return instance;
    }

    void Update()
    {
        //TODO Updates based on inputs and notifications - Biggest being death notfication
        switch (state)
        {
            case State.FIGHTING:
                break;
            case State.PAUSED:
                break;
            case State.STARTING:
                break;
            case State.ROUND_BEGINING:
                break;
            case State.ROUND_ENDING:
                break;
            case State.COMPLETED:
                break;
            case State.SUMMARIZING:
                break;
        }
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


}
