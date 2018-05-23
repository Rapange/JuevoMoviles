using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	float life;
	float t_x, t_z;
	void Start () {
		life = 100.0f;
		t_x = 0; t_z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*t_x = Time.deltaTime * Random.Range(-1,2);
		t_z = Time.deltaTime * Random.Range(-1,2);
		if(transform.position.x > 10) t_x = -1;
		if(transform.position.x < -10) t_x = 1;
		if(transform.position.z > 10) t_z = -1;
		if(transform.position.z < -10) t_z = 1;*/
		t_x = 1;
		if(transform.position.x > 10) t_x = -1;
		if(transform.position.x < -10) t_x = 1;
		transform.Translate(t_x, 0, t_z);
		if(life <= 0.0f){
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(!collision.gameObject.tag.Equals("Enemy")){
			life -= 25.0f;
		}
	}
}
