using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class StartBotton : MonoBehaviour {

	public void OnStartButtonClicked(){
		SceneManager.LoadScene ("MainGame");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
