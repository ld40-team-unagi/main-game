using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

	public GameObject Player;

	void OnCollisionEnter(Collision other){

		if (other.gameObject.name == "Player") {
			Player.GetComponent<Player> ().isDead = true;
			Debug.Log (Player.GetComponent<Player> ().isDead);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
