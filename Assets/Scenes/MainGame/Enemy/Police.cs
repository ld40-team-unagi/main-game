using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {
	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "Player") {
			other.gameObject.GetComponent<Player> ().isDead = true;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
