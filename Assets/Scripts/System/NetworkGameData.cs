using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameData : NetworkBehaviour{

    public class SyncListPlayerRecord : SyncListStruct<PlayerRecord> { }
    [SyncVar]
    public SyncListPlayerRecord networkPlayers = new SyncListPlayerRecord();


    public struct PlayerRecord
    {
        public NetworkIdentity id;
        public PlayerRecord(NetworkIdentity id)
        {
            this.id = id;
        }
    }
}
