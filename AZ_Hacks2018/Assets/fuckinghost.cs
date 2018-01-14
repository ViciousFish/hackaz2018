using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class fuckinghost : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<NetworkManager>().StartHost();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
