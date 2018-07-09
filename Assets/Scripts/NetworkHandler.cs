using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : NetworkManager
{
    private Vector3 playerSpawnPos;
    private Game game;
    
    void Awake()
    {
        game = Game.GetInstance();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!game) return;
        if(Game.GetInstance().GetGameMode() == Game.GameMode.LOCALMULTIPLAYER)
        {
            return;
        }
        var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Logger.Instance.Message(DEVELOPER.ADAM, "Player is being spawned");
    }

}
