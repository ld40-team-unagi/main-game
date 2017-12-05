using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour {
	AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			Destroy (gameObject);
		}
		
	}
}
