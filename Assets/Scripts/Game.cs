using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    
    public int rounds = 3;
    public int lives = 2;
    private bool host = true;

    private static Game instance;
    private static Player localPlayer;
    private State state = State.STARTING;
    


    void Start()
    {
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


    /*************************************************************************/

    public static Player GetPlayer()
    {
        return localPlayer;
    }

    public static Game GetInstance()
    {
        return instance;
    }

    void Update()
    {

    }
    /*************************************************************************/
    enum State
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
