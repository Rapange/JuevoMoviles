using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public Transform fire;
	public Transform water;
	public Transform _light;
	
	void Start () {

		//FindObjectOfType<Camera>().transform.localPosition; //gives the position of current player's device. (ARcamera).
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.touchCount == 1){
			Touch pTouch = Input.GetTouch(0);
			if(pTouch.phase == TouchPhase.Began){
				Transform bullet;
				if(pTouch.position.y < Screen.height / 3){
					bullet = Instantiate(fire, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
				
				}
				else if(pTouch.position.y >= Screen.height / 3 && pTouch.position.y < 2 * Screen.height / 3){
					bullet = Instantiate(_light, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
				}
				else{
					bullet = Instantiate(water, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
				}
				bullet.SetParent(GameObject.Find("ImageTarget").transform);
			}
		}
	}
}
