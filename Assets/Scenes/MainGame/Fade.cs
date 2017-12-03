using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
	public float speed = 0.01f;
	public bool enable = false;

	bool isFinish = false;
	float alpha = 0;
	float red, green, blue;
	Image image;

	public bool IsFinish(){
		return isFinish;
	}
	public void Reset(){
		alpha = 0;
		apply();
	}
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		red = image.color.r;
		green = image.color.g;
		blue = image.color.b;
	}

	// Update is called once per frame
	void Update () {
		if(!enable || isFinish){
			return;
		}
		if(alpha < 1){
			alpha += speed;
		}else{
			isFinish = true;
		}
		apply();
	}
	void apply(){
		image.color = new Color(red, green, blue, alpha);
	}
}
