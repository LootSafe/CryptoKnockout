using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkCustom : NetworkManager
{

    public Characters chosenCharacter = 0;
    public GameObject[] characters;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public Characters chosenCharacter;
        
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        Characters selectedCharacter = message.chosenCharacter;
        Debug.Log("server add with message " + chosenCharacter);
        GameObject player;
        GameObject prefab = CharacterSwapper.GetCharacter(selectedCharacter);
        Transform startPos = GameObject.FindGameObjectWithTag("P1Spawn").GetComponent<Transform>();

        if (startPos != null)
        {
            player = Instantiate(prefab, startPos.position, startPos.rotation) as GameObject;
        }
        else
        {
            player = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage spawnCharacter = new NetworkMessage();
        spawnCharacter.chosenCharacter = GlobalGameData.GetInstance().player1Char;

        ClientScene.AddPlayer(conn, 0, spawnCharacter);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1()
    {
        chosenCharacter = Characters.BJORN;
    }

    public void btn2()
    {
        chosenCharacter = Characters.DOGE;
    }
}
