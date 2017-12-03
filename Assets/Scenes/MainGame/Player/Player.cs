﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Component.camera;

public class Player : MonoBehaviour {
	
	public Camera cam;
	Rigidbody rb;
	public float camAngleY = 0f;
	public float camDistance = 5f;
	public float camHight = 5f;
	public float mushRand = 0.5f;
	public GameObject herbePot;
	public GameObject mushroomPot;


	Vector3 preVelocity;
	public int cropYields;
	public int seeds;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();
	}

	void Update () {
		cam.transform.position = transform.position + new Vector3 (
			-camDistance*Mathf.Sin(camAngleY), 
			camHight, 
			-camDistance*Mathf.Cos(camAngleY));
		cam.transform.LookAt (transform);
		camAngleY += 0.002f;

		if (Input.GetButtonDown ("Fire1")) {
			SowSeed ();
		}
	}

	public void AddCropYields(int n){
		cropYields += n;
	}

	public int CropYields{
		get{return cropYields;}
	}

	public void ResetCropYields(){
		cropYields = 0;
	}

	public void AddSeeds(int n){
		seeds += n;
	}

	public int Seeds{
		get{return seeds;}
	}

	public void ResetSeeds(){
		seeds = 0;
	}

	void SowSeed(){
		if (seeds == 0)
			return;
		Vector3 forward = transform.forward;

		if(Random.Range(0f, 1f) <= mushRand){
			mushroomPot = (GameObject)Instantiate(mushroomPot, transform.position + forward, Quaternion.identity);
		}else{
			herbePot = (GameObject)Instantiate(herbePot, transform.position + forward, Quaternion.identity);
		}

		seeds -= 1;
	}


	//void FixedUpdate(){
		//rb.velocity = new Vector3(moveX, 0, moveZ);
	//}

	void FixedUpdate() 
	{
		float speed = 100.0f;
		Vector3 localForceVector = new Vector3 (
			                           Input.GetAxis ("Horizontal") * speed,
			                           0,
			                           Input.GetAxis ("Vertical") * speed
		                           );
		Vector3 globalForceVector = Quaternion.AngleAxis (camAngleY*180f/Mathf.PI, new Vector3 (0f, 1f, 0f)) * localForceVector;
		if (localForceVector.magnitude != 0f) {
			transform.LookAt (transform.position + globalForceVector);
		}
		rb.AddForce(globalForceVector);
	}
}
