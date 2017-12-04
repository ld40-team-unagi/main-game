using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class StartBotton : MonoBehaviour {
	AudioSource audioSource;
	public void OnStartButtonClicked(){
		audioSource.Play();
		SceneManager.LoadScene ("MainGame");
	}
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
