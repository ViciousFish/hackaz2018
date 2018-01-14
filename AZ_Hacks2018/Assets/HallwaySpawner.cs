using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HallwaySpawner : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("oogabooga");
		//attach ourself to the ARMazeContainer
		Debug.Log(Network.isServer);
		Debug.Log (Network.isClient);
//		if (Network.isClient) {
		GameObject go = GameObject.FindWithTag("ARM");
		if (go != null) {
			this.transform.parent = go.transform;
//			Vector3 global = this.transform.position;
			this.transform.localPosition = this.transform.position * 2.64f;

//			this.transform.position = this.transform.position - go.GetComponent<AccessibleOffset>().offsetFromOrigin;
		}

//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
