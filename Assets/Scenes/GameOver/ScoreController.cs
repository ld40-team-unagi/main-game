using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = ScoreCounte.CurrentScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
