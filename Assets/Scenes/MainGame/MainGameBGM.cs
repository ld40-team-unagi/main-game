using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameBGM : MonoBehaviour {
	Player player;
	AudioSource source;
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		source = GetComponent<AudioSource>();
	}
	
	void Update () {
		if(player.isProduced && !source.isPlaying){
			source.Play();
		}
	}
}
