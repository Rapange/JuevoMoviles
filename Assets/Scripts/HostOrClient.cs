using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostOrClient : MonoBehaviour {

	private NetworkManager manager;
	// Use this for initialization
	void Start () {
		manager = GetComponent<NetworkManager>();
		if(OnlineInit.option == 1){
			manager.StartHost();
		}
		else manager.StartClient();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
