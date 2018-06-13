using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

	// Use this for initialization
	private float life;
	private float t_x, t_z;
	private GameObject master;
	private float speed;
	
	void Start () {
		life = 100.0f;
		speed = 4.0f;
		t_x = speed * Time.deltaTime; t_z = 2;
		master = GameObject.Find("Master");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(transform.position.x > 5) t_x = -speed * Time.deltaTime;
		if(transform.position.x < -5) t_x = speed * Time.deltaTime;
		if(transform.position.z > 5) t_z = -speed * Time.deltaTime;
		if(transform.position.z < -5) t_z = speed * Time.deltaTime;
		transform.Translate(t_x, 0, t_z);
		
	}
	
	void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("Enemy")){
			life -= 25.0f;
			if(collision.gameObject.tag.Equals("Water_attack")){
			   	t_x = t_x / 2;
				transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", new Color32(30,144,255,25));
			}
			
			transform.GetChild(2).GetComponent<ParticleSystem>().Play();
			ParticleSystem.EmissionModule em = transform.GetChild(2).GetComponent<ParticleSystem>().emission;
			em.enabled = true;
			Destroy(collision.gameObject);
		}
		
		if(life <= 0.0f){
			Destroy(gameObject);
			master.GetComponent<Master>().enemies--;
		}
	}
}
