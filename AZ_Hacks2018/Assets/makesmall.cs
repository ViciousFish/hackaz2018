using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class makesmall : NetworkBehaviour {

	int count = 0;
	// Use this for initialization
	void Start () {
		Debug.Log ("going to make tiny soon!");
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		if (count == 50) {
			this.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
		}
	}
}
