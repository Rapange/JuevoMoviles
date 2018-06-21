using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Eje y es el eje z en el celular.
		GetComponent<Rigidbody>().velocity = new Vector3(5*Input.acceleration.y,0.0f, 0.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Transform>().position.y < -0.5){
			Destroy(gameObject);
		}
		
	}
}
