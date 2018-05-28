using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(5*Input.acceleration.y,0.0f, -5*Input.acceleration.x);
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Transform>().position.y < -0.5){
			Destroy(gameObject);
		}
		
	}
}
