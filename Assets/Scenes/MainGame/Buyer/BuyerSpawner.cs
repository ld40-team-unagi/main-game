using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerSpawner : MonoBehaviour {
	public GameObject spawnedPrefab;
	public float rangeRadius;

	GameObject spawned;

	// Update is called once per frame
	void Update () {
		if (spawned == null) {
			Spawn ();
		}
	}

	void Spawn(){
		spawned = Instantiate(spawnedPrefab, spawningPosition(), Quaternion.identity);
		spawned.transform.eulerAngles = new Vector3 (0f, Random.Range (0,360f), 0f);
	}

	Vector3 spawningPosition(){
		Vector3 direction = new Vector3 (
			Random.Range (-1f, 1f),
			Random.Range (-1f, 1f),
			Random.Range (-1f, 1f)
		).normalized;
		Vector3 position = direction * Random.value * rangeRadius;
		Vector3 flatted = Vector3.Scale (position, new Vector3 (1f, 0f, 1f));
		return flatted + transform.position;
	}
}
