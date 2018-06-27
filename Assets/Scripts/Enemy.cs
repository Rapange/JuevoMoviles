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
	
	[SyncVar]//dfs
	public bool isFrozen;
	[SyncVar]
	public bool isBurning;
	
	ParticleSystem.EmissionModule em;
		
	void Start () {
		GetComponent<Animator>().Play(0);
		transform.Rotate(Vector3.up, 90);
		
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
		//Debug.Log(isFrozen);

		if(isFrozen){
			//Debug.Log("enem frozen");
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
			GetComponent<Animator>().enabled = true;
			speed = 4.0f;
			lightStart = 9999999;
		}

		transform.Rotate(Vector3.up, Random.Range(-270f,270f) * Time.deltaTime);
		if(transform.position.x  > 5 || transform.position.x < -5 || transform.position.z > 5 || transform.position.z < -5) {
			//isNegative = -isNegative; 
			transform.Rotate(Vector3.up, -180);
			//Debug.Log("entra");
		}
		transform.Translate(0,0,speed * Time.deltaTime);
		//Debug.Log(transform.position);
		
		
		/*if(transform.position.z > 5) transform.Translate(0, 0, -speed * Time.deltaTime); 
		if(transform.position.z < -5) transform.Translate(0, 0, speed * Time.deltaTime);*/
		
		
	}
	

	
	[Command]
	void CmdSound(int sound){
		Debug.Log("server sound");
		isFrozen = true;
		AudioSource[] list = GetComponents<AudioSource>();
		list[sound].Play();
		
		RpcSound(sound);
	}
	
	[ClientRpc]
	public void RpcSound(int sound){
		AudioSource[] list = GetComponents<AudioSource>();
		list[sound].Play();
		Debug.Log("sound");
	}
	
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag.Equals("Wall")){
			transform.Rotate(Vector3.up,-180.0f);
		}
		
		if(!collision.gameObject.tag.Equals("Enemy") && !collision.gameObject.tag.Equals("Wall")){
			
			if(collision.gameObject.tag.Equals("Water_attack")){
				isFrozen = true;
				waterStart = Time.timeSinceLevelLoad;
				/*GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CmdSound(1);*/
				
				RpcSound(1);
			}
			else if(collision.gameObject.tag.Equals("Fire_attack")){
			
				isBurning = true;
				fireStart = Time.timeSinceLevelLoad;
				/*GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CmdSound(0);
				CmdSound(0);*/
				RpcSound(0);
			}
			else{
				GetComponent<Animator>().enabled = false;
				lightStart = Time.timeSinceLevelLoad;
				speed = 0.0f;
				life -= 10.0f;
				/*GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CmdSound(2);
				CmdSound(2);*/
				
				RpcSound(2);
			}
			Destroy(collision.gameObject);
		}
		
		if(life <= 0.0f){
			//Destroy(gameObject);
			//master.GetComponent<Master>().enemies--;
			//RpcSound(3);
			master.GetComponent<Master>().kill(gameObject);
			
		}
		
		
	}
}
