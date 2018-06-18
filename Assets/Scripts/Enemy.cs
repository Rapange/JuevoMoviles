using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

	// Use this for initialization
	private float life;
	private GameObject master;
	private float speed;
	private float fireStart;
	private float waterStart;
	private float lightStart;
	private float now;
	private float isNegative;
	
	void Start () {
		life = 100.0f;
		speed = 4.0f;
		master = GameObject.Find("Master");
		fireStart = waterStart = lightStart = -4.0f;
		isNegative = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		now = Time.timeSinceLevelLoad;
		if(now - fireStart < 2.0f){
			life -= 10.0f;
		}
		else{
			transform.GetChild(2).GetComponent<ParticleSystem>().Stop();
			ParticleSystem.EmissionModule em = transform.GetChild(2).GetComponent<ParticleSystem>().emission;
			em.enabled = false;
		}
		if(now - waterStart >= 2.0f){
			speed = 4.0f;
			CmdChangeColor(false);
			waterStart = 9999999;
		}
		if(now - lightStart >= 0.5f){
			speed = 4.0f;
			lightStart = 9999999;
		}

	
		if(transform.position.x > 5 || transform.position.x < -5) isNegative = -isNegative;
		transform.Translate(isNegative*speed * Time.deltaTime,0,0);
		
		/*if(transform.position.z > 5) transform.Translate(0, 0, -speed * Time.deltaTime); 
		if(transform.position.z < -5) transform.Translate(0, 0, speed * Time.deltaTime);*/
		
		
	}
	
	[Command]
	void CmdChangeColor(bool opt){
		if(opt) transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", new Color32(30,144,255,255));
		else transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", new Color32(255,255,255,255));
	}
	
	void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("Enemy")){
			//life -= 25.0f;
			if(collision.gameObject.tag.Equals("Water_attack")){
				speed = 2.0f;
				CmdChangeColor(true);
				waterStart = Time.timeSinceLevelLoad;
			}
			else if(collision.gameObject.tag.Equals("Fire_attack")){
			
				transform.GetChild(2).GetComponent<ParticleSystem>().Play();
				ParticleSystem.EmissionModule em = transform.GetChild(2).GetComponent<ParticleSystem>().emission;
				em.enabled = true;
				fireStart = Time.timeSinceLevelLoad;
			}
			else{
				lightStart = Time.timeSinceLevelLoad;
				speed = 0.0f;
				life -= 10.0f;
			}
			Destroy(collision.gameObject);
		}
		
		if(life <= 0.0f){
			Destroy(gameObject);
			master.GetComponent<Master>().enemies--;
		}
	}
}
