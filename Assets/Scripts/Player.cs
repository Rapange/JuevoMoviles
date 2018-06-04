using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	// Use this for initialization
	public Transform fire;
	public Transform water;
	public Transform _light;
	private Transform imageTarget;
	
	void Start () {

		/*me = GameObject.CreatePrimitive(PrimitiveType.Cube);
		me.transform.position = new Vector3(0,0,0);
		me.transform.SetParent(GameObject.Find("ImageTarget").transform);*/
		imageTarget = GameObject.Find("ImageTarget").transform;
		
		//FindObjectOfType<Camera>().transform.localPosition; //gives the position of current player's device. (ARcamera).
	}
	
	// Update is called once per frame
	void Update () {

		if(!isLocalPlayer){
			return;
		}
		transform.position = new Vector3(FindObjectOfType<Camera>().transform.localPosition.x, FindObjectOfType<Camera>().transform.localPosition.y, - FindObjectOfType<Camera>().transform.localPosition.z);
		if(Input.touchCount == 1){
			Touch pTouch = Input.GetTouch(0);
			if(pTouch.phase == TouchPhase.Began){
				//Transform bullet;
				if(pTouch.position.y < Screen.height / 3){
					//bullet = Instantiate(fire, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					CmdFire(1, FindObjectOfType<Camera>().transform.localPosition);
				}
				else if(pTouch.position.y >= Screen.height / 3 && pTouch.position.y < 2 * Screen.height / 3){
					//bullet = Instantiate(_light, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					CmdFire(2, FindObjectOfType<Camera>().transform.localPosition);
				}
				else{
					//bullet = Instantiate(water, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					CmdFire(3, FindObjectOfType<Camera>().transform.localPosition );
				}

				/* bullet.SetParent(imageTarget);
 				 *NetworkServer.Spawn(bullet.gameObject);
				 *
				 */
			}
		}
	}
	
	[Command]
	void CmdFire(int type, Vector3 pos){//
		Transform bullet;
		if(type == 1)
			bullet = Instantiate(fire, pos, Quaternion.identity);
		else if(type == 2)
			bullet = Instantiate(_light, pos, Quaternion.identity);
		else
			bullet = Instantiate(water, pos, Quaternion.identity);
			

		bullet.SetParent(imageTarget);
		NetworkServer.Spawn(bullet.gameObject);
	}
}
