﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	private float life;
	private float t_x, t_z;
	private GameObject master;
	
	void Start () {
		life = 100.0f;
		t_x = 1 * Time.deltaTime; t_z = 0;
		master = GameObject.Find("Master");
	}
	
	// Update is called once per frame
	void Update () {
		/*t_x = Time.deltaTime * Random.Range(-1,2);
		t_z = Time.deltaTime * Random.Range(-1,2);
		if(transform.position.x > 10) t_x = -1;
		if(transform.position.x < -10) t_x = 1;
		if(transform.position.z > 10) t_z = -1;
		if(transform.position.z < -10) t_z = 1;*/
		
		if(transform.position.x > 5) t_x = -1 * Time.deltaTime;
		if(transform.position.x < -5) t_x = 1 * Time.deltaTime;
		transform.Translate(t_x, 0, t_z);
		
	}
	
	void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("Enemy")){
			life -= 25.0f;
		}
		
		if(life <= 0.0f){
			Destroy(gameObject);
			master.GetComponent<Master>().enemies--;
		}
	}
}
