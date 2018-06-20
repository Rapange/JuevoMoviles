using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

	// Use this for initialization
	[SyncVar]
	public float life;
	private GameObject master;
	
	[SyncVar]
	public float speed;
	
	[SyncVar]
	public float fireStart;
	
	[SyncVar]
	public float waterStart;
	
	[SyncVar]
	public float lightStart;
	private float now;
	private float isNegative;
	
	[SyncVar]
	public bool isFrozen;
	[SyncVar]
	public bool isBurning;
	
	ParticleSystem.EmissionModule em;
		
	void Start () {
		life = 100.0f;
		speed = 4.0f;
		master = GameObject.Find("Master");
		fireStart = waterStart = lightStart = -4.0f;
		isNegative = 1.0f;
		isFrozen = false;
		isBurning = false;
		em = transform.GetChild(2).GetComponent<ParticleSystem>().emission;
		em.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		now = Time.timeSinceLevelLoad;
		

		if(isFrozen){
			speed = 2.0f;
			transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", new Color32(30,144,255,255));
		}
		else{
			transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", new Color32(255,255,255,255));
		}
		
		if(isBurning){
			life -= 10.0f * Time.deltaTime;
			if(!em.enabled){
				transform.GetChild(2).GetComponent<ParticleSystem>().Play();
				
				em.enabled = true;
			}
		}
		else{
			if(em.enabled){
				transform.GetChild(2).GetComponent<ParticleSystem>().Stop();

				em.enabled = false;
			}
		}
		
		if(now - fireStart >= 2.0f){
			isBurning = false;
		}
		if(now - waterStart >= 2.0f){
			speed = 4.0f;
			isFrozen = false;
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
	
	
	void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("Enemy")){
			//life -= 25.0f;
			if(collision.gameObject.tag.Equals("Water_attack")){
				isFrozen = true;
				waterStart = Time.timeSinceLevelLoad;
			}
			else if(collision.gameObject.tag.Equals("Fire_attack")){
			
				isBurning = true;
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
			//Destroy(gameObject);
			//master.GetComponent<Master>().enemies--;
			master.GetComponent<Master>().kill(gameObject);
		}
	}
}
