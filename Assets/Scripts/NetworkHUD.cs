using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHUD : NetworkManagerHUD {


	// Use this for initialization
	void Start () {
        manager.matchHost = "10.10.10.48";
        manager.matchPort = 55777;
        manager.serverBindAddress = "0.0.0.0";
        manager.serverBindToIP = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
