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
		enemies = 0;
		cont = 1;
		imageTarget = GameObject.Find("ImageTarget").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies == 0){
			enemies = cont;
			cont++;
			for(int i = 0; i < enemies; i++){
				Transform enemy = Instantiate(birdon,new Vector3(Random.Range(-4f,4f),0,Random.Range(-4f,4f)), Quaternion.identity);
				
				//enemy.SetParent(imageTarget);
				//ClientScene.RegisterPrefab(enemy.gameObject);
				NetworkServer.Spawn(enemy.gameObject);
			}
			
		}
		
		/*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++){
			for(int j = i+1; j < players.Length; j++){
				if(players[i].GetComponent<Player>().id == 0 && players[j].GetComponent<Player>().id != players[i].GetComponent<Player>().id){
					//if(Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x) <= 10 && Mathf.Abs(players[i].transform.position.y - players[j].transform.position.y) <= 10){
						players[i].GetComponent<Player>().id = 3;
					//}
				}
			}
			
		}*/
	}
	
	public void kill(GameObject enem){
		Destroy(enem);
		enemies--;
	}
}
