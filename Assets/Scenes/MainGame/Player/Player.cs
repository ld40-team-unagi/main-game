using System.Collections;
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
	public bool isDead = false;
	public GameObject herbePot;
	public GameObject mushroomPot;
	public bool isProduced = false;


	Vector3 preVelocity;
	public int cropYields;
	public int seeds;
	bool inHouse = false;

	Transform head;

   	float mouseXSpeed = 0f;
	float zoom = 0.5f;
	float zoomSpeed = 0f;

	float backPackMass = 1f;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		head = transform.Find ("Head");
	}

	void Update () {
		mouseXSpeed *= 0.97f;
		mouseXSpeed += Input.GetAxis ("Mouse X") * 0.002f;
		camAngleY += mouseXSpeed;
		head.transform.eulerAngles = new Vector3 (0f, camAngleY*180f/Mathf.PI, 0f);
		Vector3 headPosition = transform.position + new Vector3 (0f, 1f, 0f);
		float zoomProxi;
//		if (inHouse) {
//			zoomProxi = 0.9f;
//		}else{
//			if (zoom <= 0f || 0.9f <= zoom) {
//				zoomSpeed = 0f;
//			} else {
//				zoomSpeed *= 0.97f;
//			}
//			zoomSpeed += Input.GetAxis ("Mouse ScrollWheel") * 0.0005f;
//
//			zoom =+ Mathf.Clamp(zoom - zoomSpeed*80f,0f,0.9f);
//
//			zoomProxi = zoom;
//		}

		if (zoom <= 0f || 0.9f <= zoom) {
			zoomSpeed = 0f;
		} else {
			zoomSpeed *= 0.97f;
		}
		zoomSpeed += Input.GetAxis ("Mouse ScrollWheel") * 0.0005f;

		zoom =+ Mathf.Clamp(zoom - zoomSpeed*80f,0f,0.9f);
		
		zoomProxi = zoom;


		float zoomX = (1f + zoomProxi*2f) * Mathf.Cos (zoomProxi * Mathf.PI * 0.5f);
		float zoomY = (1f + zoomProxi*2f) * Mathf.Sin (zoomProxi * Mathf.PI * 0.5f);
//		if (inHouse) {
//			cam.transform.position = headPosition + new Vector3 (
//				-camDistance*0.1f*Mathf.Sin(camAngleY), 
//				camHight*4f, 
//				-camDistance*0.1f*Mathf.Cos(camAngleY)
//			)*zoomProxi;
//			cam.transform.eulerAngles = new Vector3(90f,0f,0f);
//		} else {
//			cam.transform.position = headPosition + new Vector3 (
//				-camDistance*Mathf.Sin(camAngleY)*zoomX, 
//				camHight*zoomY, 
//				-camDistance*Mathf.Cos(camAngleY)*zoomX
//			);
//		}

		cam.transform.position = headPosition + new Vector3 (
			-camDistance*Mathf.Sin(camAngleY)*zoomX, 
			camHight*zoomY, 
			-camDistance*Mathf.Cos(camAngleY)*zoomX
		);

		cam.transform.LookAt (headPosition);

		if (Input.GetButtonDown ("Fire1")) {
			isProduced = true;
			SowSeed ();
		}

		if (isDead) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}else{
			isDead = isProduced && seeds == 0 && GameObject.FindGameObjectsWithTag ("Pot").Length == 0;
		}

		UpdateBag ();
	}

	void UpdateBag(){
		GameObject backPack = transform.FindChild ("BackPack").gameObject;
		float scale;
		if (cropYields > 0) {
			backPackMass = 1f+cropYields * 0.0005f;
			scale = backPackMass;
		} else {
			backPackMass = 1f;
			scale = 0f;
		}

		backPack.transform.localScale = new Vector3 (scale,scale,scale);
	}

	void OnTriggerEnter(Collider c){
		GameObject target = c.gameObject;
		if (target.GetComponent<House> () != null) {
			inHouse = true;
		}

		if (target.tag == "Buyer") {
			if (cropYields == 0) {
				target.GetComponent<Buyer> ().RingNoMoney();
				return;
			}
				
			ScoreCounte.AddScore((uint)cropYields);
			cropYields = 0;
			target.GetComponent<Buyer> ().RingMoney();
			Destroy (target);
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
			Instantiate(mushroomPot, transform.position + forward, Quaternion.identity);
		}else{
			Instantiate(herbePot, transform.position + forward, Quaternion.identity);
		}

		seeds -= 1;
	}


	//void FixedUpdate(){
		//rb.velocity = new Vector3(moveX, 0, moveZ);
	//}

	void FixedUpdate() 
	{
		if (!isDead) {
			float speed = 100.0f;
			Vector3 localForceVector = new Vector3 (
				                           Input.GetAxis ("Horizontal"),
				                           0,
				                           Input.GetAxis ("Vertical")
			                           ).normalized*speed;
			Vector3 globalForceVector = Quaternion.AngleAxis (camAngleY * 180f / Mathf.PI, new Vector3 (0f, 1f, 0f)) * localForceVector;
			if (localForceVector.magnitude != 0f) {
				transform.LookAt (transform.position + globalForceVector);
			}
			rb.AddForce (globalForceVector/(backPackMass));


			inHouse = false;
		} else {
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
