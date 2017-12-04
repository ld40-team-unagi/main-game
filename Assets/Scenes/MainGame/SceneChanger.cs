using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public GameObject player;
	GameObject fadePanel;

	void Start(){
		fadePanel = GameObject.Find ("FadePanel");
	}

	// Update is called once per frame
	void Update () {
		GameOver ();
	}

	public void GameOver(){
	
		bool flag = player.GetComponent<Player> ().isDead;
		if (flag == true) {

			fadePanel.GetComponent<Fade> ().Enable();

			if(fadePanel.GetComponent<Fade> ().IsFinish() == true){
				SceneManager.LoadScene ("GameOver");
			}
		}
	}
}

