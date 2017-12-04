using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE: MonoBehaviour{
	public static void Play(AudioClip clip){
		AudioSource source = GameObject.Find("SE").GetComponent<AudioSource>();
		source.clip = clip;
		source.Play();
	}
}
