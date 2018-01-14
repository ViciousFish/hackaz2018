using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
	public GameObject indicatorPrefab;
	GameObject indicator = null;
	void Start(){
		indicator = (GameObject)Instantiate(indicatorPrefab);
		Debug.Log("hello");
		NetworkServer.Spawn(indicator);
	}
	void FixedUpdate()
	{
		// if (!isLocalPlayer)
		// {
		// 	return;
		// }
		// var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		// var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		// transform.Rotate(0, x, 0);
		// transform.Translate(0, 0, z);
		indicator.transform.position = Camera.main.transform.position + new Vector3(0f,1f,0f);
	}
}