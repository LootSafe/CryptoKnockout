﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameData : NetworkBehaviour{

    public class SyncListPlayerRecord : SyncListStruct<PlayerRecord> { }
    public SyncListPlayerRecord networkPlayers;

    public static NetworkGameData instance;

    public void Start()
    {
        instance = this;
    }
    public struct PlayerRecord
    {
        public NetworkIdentity id;
        public PlayerRecord(NetworkIdentity id)
        {
            this.id = id;
        }
    }

 
}
