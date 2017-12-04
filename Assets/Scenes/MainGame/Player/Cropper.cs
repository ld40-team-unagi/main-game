using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cropper : MonoBehaviour {
	// Use this for initialization
	public GameObject player;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider c){
		GameObject target = c.gameObject;
		if (target.GetComponent<Pot> () != null) {
			OnTriggerStayWithPot (target);
		}
	}

	void OnTriggerStayWithPot(GameObject pot){
		if (Input.GetButtonDown ("Fire2")) {
			GameObject plant = pot.GetComponent<Pot> ().plant;
			if (plant == null)
				return;
			if (!plant.GetComponent<Plant>().Crop())
				return;
			int cropYields = plant.GetComponent<Plant> ().cropYields;
			player.GetComponent<Player>().AddCropYields(cropYields);
			int seeds = plant.GetComponent<Plant> ().seeds;
			player.GetComponent<Player>().AddSeeds(seeds);
		}
	}
}
