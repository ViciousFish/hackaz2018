using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour {

	private Rigidbody rb;
	public int speed;
	public GameObject SpawnManagerReference;

	private int gemsCollected;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		gemsCollected = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// float moveHorizontal = Input.GetAxis("Horizontal");
		// float moveVertical = Input.GetAxis("Vertical");

		// Vector3 movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);

		// rb.AddForce(movement);
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		switch (other.gameObject.tag){
			case "gem":
				GameObject explosion = Instantiate(Resources.Load("CustomExplosion", typeof(GameObject)), other.gameObject.transform) as GameObject;
				Destroy(explosion, 1);
				Destroy(other.gameObject);
				gemsCollected++;
				gemCollected();
				break;
			case "enemy1":
				GameObject explosion1 = Instantiate(Resources.Load("CustomExplosion", typeof(GameObject)), other.gameObject.transform) as GameObject;
				Destroy(explosion1, 1);
				Destroy(other.gameObject);
				SpawnManagerReference.GetComponent<SpawnManager>().EnemyKilled();
				break;
			case "enemy2":
				if(SpawnManagerReference.GetComponent<SpawnManager>().getGemsCollected() > 0){
					GameObject explosion2 = Instantiate(Resources.Load("CustomExplosion", typeof(GameObject)), other.gameObject.transform) as GameObject;
					Destroy(explosion2, 1);
					Destroy(other.gameObject);
					SpawnManagerReference.GetComponent<SpawnManager>().EnemyKilled();
				}
				break;
			case "enemy3":
				if(SpawnManagerReference.GetComponent<SpawnManager>().getGemsCollected() > 1){
					GameObject explosion3 = Instantiate(Resources.Load("CustomExplosion", typeof(GameObject)), other.gameObject.transform) as GameObject;
					Destroy(explosion3, 1);
					Destroy(other.gameObject);
					SpawnManagerReference.GetComponent<SpawnManager>().EnemyKilled();
				}
				break;
			case "enemy4":
				if(SpawnManagerReference.GetComponent<SpawnManager>().getGemsCollected() > 2){
					GameObject explosion4 = Instantiate(Resources.Load("CustomExplosion", typeof(GameObject)), other.gameObject.transform) as GameObject;
					Destroy(explosion4, 1);
					Destroy(other.gameObject);
					SpawnManagerReference.GetComponent<SpawnManager>().EnemyKilled();
				}
				break;
			default:
				break;
		}
	}

	private void gemCollected(){
		SpawnManagerReference.GetComponent<SpawnManager>().GemCollected();
		// Update gem collection
		// Update bullet/gun color

	}
}
