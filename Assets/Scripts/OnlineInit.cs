using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OnlineInit : MonoBehaviour {

	static public int option;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.touchCount == 1){
			Touch pTouch = Input.GetTouch(0);
			if(pTouch.phase == TouchPhase.Began){
				if(pTouch.position.y < Screen.height / 2){
					option = 1;
				}
				else{
					option = 2;
				}
				SceneManager.LoadScene(1);
			}
		}
	}
}
