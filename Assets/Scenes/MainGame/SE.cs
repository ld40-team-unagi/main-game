using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE: MonoBehaviour{
	public static void Play(GameObject obj, AudioClip clip){
		AudioSource source = obj.GetComponent<AudioSource>();
		source.clip = clip;
		source.Play();
	}
	public static void Play(GameObject obj){
		AudioSource source = obj.GetComponent<AudioSource>();
		source.Play();
	}
}
