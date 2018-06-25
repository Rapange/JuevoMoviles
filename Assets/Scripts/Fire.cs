using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fire : NetworkBehaviour {

	// Use this for initialization
	[SyncVar]
	public float ini;
	void Start () {
		//Eje y es el eje z en el celular. Eje x del Vector3 es el disparo de izquierda y derecha.
		//Eje x es el eje y en el celular.
		
		ini = Time.timeSinceLevelLoad;
		//GetComponent<Rigidbody>().velocity = new Vector3(-5 * Input.acceleration.x * Input.acceleration.y,0.0f, -20 * Input.acceleration.x * Input.acceleration.y);
		//GetComponent<Rigidbody>().velocity = FindObjectOfType<Camera>().transform.forward * 5;
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
	
	float mod(float x, float y, float z){
		return Mathf.Sqrt(x*x + y*y + z*z);
	}
}
