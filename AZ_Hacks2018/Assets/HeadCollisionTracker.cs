using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Transform location:/t" + gameObject.transform.position);
		//Debug.Log("Transform rotation:/t" + gameObject.transform.localEulerAngles);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log("You hit:\t" + collision.gameObject.name);
	}
}
