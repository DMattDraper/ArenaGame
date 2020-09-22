using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	//Public Members
	public float maxEnemy;
	public float reloadTime;
	public float startDelay;
	public GameObject enemy;
	public GameObject spawnDisplay;
	public List<GameObject> enemyList;
	
	//Private Members
	private float enemyCount = 0;
	private bool loaded = false;
	private bool depleted = false;
	private WaveController wc;
	
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartReload");
		StartCoroutine("StartAnimateSpawn");
		enemyList = new List<GameObject>();
		wc = gameObject.GetComponentInParent<WaveController>();
    }

    // Update is called once per frame
    void Update()
    {
		// Function while not depleted
		if (!depleted){
			
			// Is the spawner depleted?
			if (enemyCount == maxEnemy && enemyList.Count == 0){
				depleted = true;
				wc.incrementDepletion();
				//Debug.Log("This bitch empty!");
			}
			
			// Is it time to spawn?
			if (loaded && enemyCount < maxEnemy) Spawn();
			
			// Are there enemies in the list that have been le killed?
			for (int i = 0; i < enemyList.Count; i++){
				if (enemyList[i] == null){
					enemyList.RemoveAt(i);
				}
			}
		}
    }
	
	
	// Spawn the Enemy
	void Spawn(){
		enemyList.Add(Instantiate(enemy, transform.position, transform.rotation));
		enemyCount++;
		loaded = false;
		StartCoroutine("Reload");
		if (enemyCount < maxEnemy) StartCoroutine("AnimateSpawn");
	}
	
	// Reload the spawner
	IEnumerator Reload(){
		yield return new WaitForSeconds(reloadTime);
		loaded = true;
	}
	
	// Display something before the enemy spawns
	IEnumerator AnimateSpawn(){
		yield return new WaitForSeconds(reloadTime - 0.5f);
		spawnDisplay.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		spawnDisplay.SetActive(false);
	}
	
	// Reload the spawner
	IEnumerator StartReload(){
		yield return new WaitForSeconds(startDelay);
		loaded = true;
	}
	
	// Display something before the enemy spawns
	IEnumerator StartAnimateSpawn(){
		yield return new WaitForSeconds(startDelay - 0.5f);
		spawnDisplay.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		spawnDisplay.SetActive(false);
	}
}
