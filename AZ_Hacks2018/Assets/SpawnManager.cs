using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {


	private List<GameObject> gems;
	private List<GameObject> enemies1;
	private List<GameObject> enemies2;
	private List<GameObject> enemies3;
	private List<GameObject> enemies4;
	private List<GameObject> gemInstances;

	private int tier1Spawned;
	private int tier2Spawned;
	private int tier3Spawned;
	private int tier4Spawned;

	private int gemsCollected;

	public int maxEnemies;

	// Use this for initialization
	void Start () {
		tier1Spawned = 0;
		tier2Spawned = 0;
		tier3Spawned = 0;
		tier4Spawned = 0;

		gemsCollected = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void StartManagement() {
		FillArrays();

		int count = gems.Count;

		gemInstances = new List<GameObject>();
		for(int i = 0; i < count; i++){
			GameObject gem = Instantiate(Resources.Load("JamesTesting/Prefabs/Gem", typeof(GameObject))) as GameObject;
			gem.transform.localPosition = gems[i].transform.localPosition;
			gem.transform.localPosition = new Vector3(gem.transform.localPosition.x,
														0.8f,
														gem.transform.localPosition.z);
			gem.tag = "gem";
			gemInstances.Add(gem);
		}

		int numT1AtStart = (int) maxEnemies - Random.Range(1, maxEnemies / 2);
		int numT2AtStart = maxEnemies - numT1AtStart;

		// Spawn tier 1 enemies
		for(int i = 0; i < numT1AtStart; i++){
			GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy1", typeof(GameObject))) as GameObject;
			enemy.transform.localPosition = enemies1[tier1Spawned].transform.localPosition;
			enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
														1.5f,
														enemy.transform.localPosition.z);
			enemy.tag = "enemy1";
			tier1Spawned++;
		}

		// Spawn tier 2 enemies
		for(int i = 0; i < numT2AtStart; i++){
			GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy2", typeof(GameObject))) as GameObject;
			enemy.transform.localPosition = enemies2[tier2Spawned].transform.localPosition;
			enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
														1.5f,
														enemy.transform.localPosition.z);
			enemy.tag = "enemy2";
			tier2Spawned++;
		}
	}

	public void GemCollected(){
		gemsCollected++;


		if(gemsCollected == 1){
			for (int i = 0; i < gems.Count; i++){
				Debug.Log("==============Gem");
				if(gemInstances[i] != null){
					gemInstances[i].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
					foreach (Transform child in gemInstances[i].transform)
					{
						child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
					}
				}
			}
		}else if(gemsCollected == 2){
			for (int i = 0; i < gems.Count; i++){
				Debug.Log("==============Gem");
				if(gemInstances[i] != null){
					gemInstances[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
					foreach (Transform child in gemInstances[i].transform)
					{
						child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
					}
				}
			}
		}
		
	}

	public void EnemyKilled(){
		Debug.Log("=============================================");

		bool enemySpawned = false;

		List<int> validTiers = new List<int>();
		if(tier1Spawned < enemies1.Count){
			validTiers.Add(0);
		}
		if(tier2Spawned< enemies2.Count && gemsCollected > 0){
			validTiers.Add(1);
		}
		if(tier3Spawned < enemies3.Count && gemsCollected > 1){
			validTiers.Add(2);
		}
		if(tier4Spawned < enemies4.Count && gemsCollected > 2){
			validTiers.Add(3);
		}
		while(!enemySpawned){

			int chooseTier = validTiers[(int) Random.Range(0, validTiers.Count)];

			Debug.Log("choose tier: " + chooseTier);
			switch(chooseTier){
				case 0:
					if(tier1Spawned < enemies1.Count){
						Debug.Log("Tier 1");
						GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy1", typeof(GameObject))) as GameObject;
						enemy.transform.localPosition = enemies1[tier1Spawned].transform.localPosition;
						enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
																	1.5f,
																	enemy.transform.localPosition.z);
						enemy.tag = "enemy1";
						tier1Spawned++;

						enemySpawned = true;
					}
					break;
				case 1:
					if(tier2Spawned < enemies2.Count && gemsCollected > 0){
						Debug.Log("Tier 2");
						GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy2", typeof(GameObject))) as GameObject;
						enemy.transform.localPosition = enemies2[tier2Spawned].transform.localPosition;
						enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
																	1.5f,
																	enemy.transform.localPosition.z);
						enemy.tag = "enemy2";
						tier2Spawned++;

						enemySpawned = true;
					}
					break;
				case 2:
					if(tier3Spawned < enemies3.Count && gemsCollected > 2){
						Debug.Log("Tier 3");
						GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy3", typeof(GameObject))) as GameObject;
						enemy.transform.localPosition = enemies3[tier3Spawned].transform.localPosition;
						enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
																	1.5f,
																	enemy.transform.localPosition.z);
						enemy.tag = "enemy3";
						tier3Spawned++;

						enemySpawned = true;
					}
					break;
				case 3:
					if(tier4Spawned < enemies4.Count && gemsCollected > 2){
						Debug.Log("Tier 4");
						GameObject enemy = Instantiate(Resources.Load("JamesTesting/Prefabs/Enemy4", typeof(GameObject))) as GameObject;
						enemy.transform.localPosition = enemies4[tier4Spawned].transform.localPosition;
						enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x,
																	1.5f,
																	enemy.transform.localPosition.z);
						enemy.tag = "enemy4";
						tier4Spawned++;

						enemySpawned = true;
					}
					break;
				default:
					break;
			}
		}
	}

	public int getGemsCollected(){
		return gemsCollected; 
	}

	private void FillArrays(){
		gems = new List<GameObject>();
		enemies1 = new List<GameObject>();
		enemies2 = new List<GameObject>();
		enemies3 = new List<GameObject>();
		enemies4 = new List<GameObject>();

		int children = transform.childCount;
		for(int i = 0; i < children; i++){
			switch(transform.GetChild(i).gameObject.tag){
				case "gem":
					gems.Add(transform.GetChild(i).gameObject);
					break;
				case "enemyTier1":
					enemies1.Add(transform.GetChild(i).gameObject);
					break;
				case "enemyTier2":
					enemies2.Add(transform.GetChild(i).gameObject);
					break;
				case "enemyTier3":
					enemies3.Add(transform.GetChild(i).gameObject);
					break;
				case "enemyTier4":
					enemies4.Add(transform.GetChild(i).gameObject);
					break;
				default:
					break;

			}
		}
	}
}
