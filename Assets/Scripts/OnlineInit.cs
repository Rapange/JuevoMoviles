using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OnlineInit : MonoBehaviour {

	static public int option;
	static public string ip;
	public Text input_field;
	public Text x_val, y_val, z_val, c_val;
	// Use this for initialization
	void Start () {
		Input.compass.enabled = true;
		option = 0;
	}
	
	// Update is called once per frame
	void Update () {
		x_val.text = Input.acceleration.x.ToString();
		y_val.text = Input.acceleration.y.ToString();
		z_val.text = Input.acceleration.z.ToString();
		c_val.text = Input.compass.magneticHeading.ToString();
		if(Input.touchCount == 1){
			Touch pTouch = Input.GetTouch(0);
			if(pTouch.phase == TouchPhase.Began){
				if(pTouch.position.y < Screen.height / 2){
					option = 1;
				}
				else if(pTouch.position.y >= Screen.height / 2 && pTouch.position.x > Screen.width / 2){
					option = 2;
					ip = input_field.text;
					Debug.Log(ip);

				}
				if(option != 0) SceneManager.LoadScene(1);
			}
		}
		
		if(Input.GetKeyDown("space")){
			option = 1;
			SceneManager.LoadScene(1);
		}
		if(Input.GetKeyDown("up")){
			option = 2;
			ip = input_field.text;
			Debug.Log(ip);
			SceneManager.LoadScene(1);
		}
	}
	

}
