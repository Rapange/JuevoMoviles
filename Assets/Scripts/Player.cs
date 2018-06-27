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
	public uint id;
	private int fireAmmo, waterAmmo, lightAmmo;
	public Transform A1,A2,A3;
	
	void Start () {

		/*me = GameObject.CreatePrimitive(PrimitiveType.Cube);
		me.transform.position = new Vector3(0,0,0);
		me.transform.SetParent(GameObject.Find("ImageTarget").transform);*/
		imageTarget = GameObject.Find("ImageTarget").transform;

		//NetworkConnection nc = GetComponent<NetworkConnection>();
		id = netId.Value;
		
		if(isLocalPlayer){
			if(id == 3) Instantiate(A1);
			else if(id == 4) Instantiate(A2);
			else if(id == 5) Instantiate (A3);
		}
		fireAmmo = waterAmmo = lightAmmo = 3;
		//FindObjectOfType<Camera>().transform.localPosition; //gives the position of current player's device. (ARcamera).
	}
	
	// Update is called once per frame
	void Update () {

		if(!isLocalPlayer){
			return;
		}
		
		
		
		transform.position = FindObjectOfType<Camera>().transform.position;
		transform.rotation = FindObjectOfType<Camera>().transform.rotation;

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		//Debug.Log(string.Format("Total jugadores: {0}", players.Length));
		foreach(GameObject player in players){
			//Debug.Log(string.Format("ID: {0}",player.GetComponent<Player>().id));
			if(player.GetComponent<Player>().id != id){
				
				if(Vector3.Distance(player.transform.position,transform.position) < 5.0f){
					//Debug.Log("entra");
					if(player.GetComponent<Player>().id == 3){
						fireAmmo = 3;
						//Debug.Log("fire!");
						
					}
					else if(player.GetComponent<Player>().id == 4){
						lightAmmo = 3;
						//Debug.Log("light!");
						
					}
					else if(player.GetComponent<Player>().id == 5){
						waterAmmo = 3;
					}
					if(!GetComponent<AudioSource>().isPlaying)
						GetComponent<AudioSource>().Play();
				}
			}
		}
		
		if(Input.touchCount == 1){
			Touch pTouch = Input.GetTouch(0);
			if(pTouch.phase == TouchPhase.Began){
				//Transform bullet;
				if(pTouch.position.y < Screen.height / 3){
					//bullet = Instantiate(fire, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					if(id != 3) fireAmmo--;
					if(fireAmmo >= 0)
						CmdFire(1, transform.position, transform.forward);
				}
				else if(pTouch.position.y >= Screen.height / 3 && pTouch.position.y < 2 * Screen.height / 3){
					//bullet = Instantiate(_light, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					if(id != 4) lightAmmo--;
					if(lightAmmo >= 0)
						CmdFire(2, transform.position, transform.forward);
				}
				else if(pTouch.position.y >= 2 * Screen.height / 3){
					//bullet = Instantiate(water, FindObjectOfType<Camera>().transform.localPosition, Quaternion.identity);
					if(id != 5) waterAmmo--;
					if(waterAmmo >= 0)
						CmdFire(3, transform.position, transform.forward );
				}

				/* bullet.SetParent(imageTarget);
 				 *NetworkServer.Spawn(bullet.gameObject);
				 *
				 */
			}
		}
	}
	
	[Command]
	void CmdFire(int type, Vector3 pos, Vector3 direction){//
		Transform bullet;
		if(type == 1)
			bullet = Instantiate(fire, pos, Quaternion.identity);
		else if(type == 2)
			bullet = Instantiate(_light, pos, Quaternion.identity);
		else
			bullet = Instantiate(water, pos, Quaternion.identity);
			

		bullet.SetParent(imageTarget);
		bullet.GetComponent<Rigidbody>().velocity = direction * 10;
		NetworkServer.Spawn(bullet.gameObject);
	}
	
	[Command]
	public void CmdSound(int sound){
		Debug.Log(sound);
	}
	
}
