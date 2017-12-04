using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanSpawner : MonoBehaviour {
	public GameObject policemanPrefab;
	public GameObject targetPlayer;
	public float rangeRadius;
	public ulong spawningFrequency;

	ulong timer;


	// Update is called once per frame
	void Update () {
		if (timer % spawningFrequency == 0) {
			SpawnPoliceman ();
		}
		timer += 1;
	}

	void SpawnPoliceman (){
		GameObject policeman = Instantiate(policemanPrefab, spawningPosition(), Quaternion.identity);
		policeman.GetComponent<Enemy> ().target = targetPlayer.transform;
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
