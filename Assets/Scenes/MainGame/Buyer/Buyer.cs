using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour {

	// Use this for initialization

	public GameObject ringingPlayer;

	public void RingNoMoney(){
		AudioSource source = GetComponent<AudioSource> ();
		source.PlayOneShot(source.clip);
	}

	public void RingMoney(){
		Instantiate (ringingPlayer, transform.position, transform.rotation);
	}
}
