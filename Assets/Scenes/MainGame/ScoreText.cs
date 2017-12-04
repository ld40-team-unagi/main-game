using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	GameObject player;
	GameObject cropYieldText;
	GameObject seedsText;
	GameObject scoreText;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		cropYieldText = GameObject.Find ("CropYield");	//CropYieldのText 
		seedsText = GameObject.Find("Seeds");
		scoreText = GameObject.Find ("Score");

	}
	
	// Update is called once per frame
	void Update () {
		CropYieldScore ();
		SeedsScore ();
		Score();
	}

	void CropYieldScore(){
		int cropnum;

		cropnum = player.GetComponent<Player> ().CropYields;
		cropYieldText.GetComponent<Text> ().text = "= " + cropnum.ToString ();

	}
	void SeedsScore(){
		int seedsnum;

		seedsnum = player.GetComponent<Player> ().Seeds;
		seedsText.GetComponent<Text> ().text = "= " + seedsnum.ToString ();
	}

	void Score(){
		uint score;

		score = ScoreCounte.CurrentScore ();
		scoreText.GetComponent<Text> ().text = "= " + score.ToString ();
	}

}
