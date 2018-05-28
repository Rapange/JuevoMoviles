using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public Transform fire;
	public Transform water;
	public Transform _light;
	private GameObject me;
	private Transform imageTarget;
	
	void Start () {

		me = GameObject.CreatePrimitive(PrimitiveType.Cube);
		me.transform.position = new Vector3(0,0,0);
		
		me.transform.SetParent(GameObject.Find("ImageTarget").transform);
		
		imageTarget = GameObject.Find("ImageTarget").transform;
		//FindObjectOfType<Camera>().transform.localPosition; //gives the position of current player's device. (ARcamera).
	}
	
	// Update is called once per frame
	void Update () {

		me.transform.position = new Vector3(FindObjectOfType<Camera>().transform.localPosition.x, FindObjectOfType<Camera>().transform.localPosition.y, - FindObjectOfType<Camera>().transform.localPosition.z);
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
				bullet.SetParent(imageTarget);
			}
		}
	}
}
