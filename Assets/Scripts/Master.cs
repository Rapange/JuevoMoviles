using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Master : NetworkBehaviour {

	public Transform birdon;
	public int enemies;
	private Transform imageTarget;
	private int cont;
	// Use this for initialization
	void Start () {
		enemies = 1;
		cont = 2;
		imageTarget = GameObject.Find("ImageTarget").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies == 0){
			enemies = cont;
			cont++;
			for(int i = 0; i < enemies; i++){
				Transform enemy = Instantiate(birdon,new Vector3(0,0,-i*2), Quaternion.identity, imageTarget);
				NetworkServer.Spawn(enemy.gameObject);
			}
			
		}
	}
}
