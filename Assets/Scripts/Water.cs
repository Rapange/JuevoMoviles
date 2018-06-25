using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Water : NetworkBehaviour {

	// Use this for initialization
	[SyncVar]
	public float ini;
	void Start () {
		//GetComponent<Rigidbody>().velocity = new Vector3(5*Input.acceleration.y, 0.0f, -5*Input.acceleration.x);
		//GetComponent<Rigidbody>().velocity = FindObjectOfType<Camera>().transform.forward * 5;
		ini = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		/*if(GetComponent<Transform>().position.y < 0.5){
			Destroy(gameObject);
		}*/
		if(Time.timeSinceLevelLoad - ini  > 3){
			Destroy(gameObject);
		}
		
	}
}