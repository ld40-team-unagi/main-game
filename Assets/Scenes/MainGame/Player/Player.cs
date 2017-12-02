using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Component.camera;

public class Player : MonoBehaviour {
	
	public Camera camera;
	[SerializeField]
	private float movement = 3f;
	[SerializeField]
	private float rotateSpeed = 2f;
	float moveX = 0f, moveZ = 0f;
	Rigidbody rb;
	public float camAngleY = 0f;
	public float camDistance = 5f;
	public float camHight = 5f;

	Vector3 preVelocity;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();
	}

	void Update () {
		camera.transform.position = transform.position + new Vector3 (
			-camDistance*Mathf.Sin(camAngleY), 
			camHight, 
			-camDistance*Mathf.Cos(camAngleY));
		camera.transform.LookAt (transform);
		camAngleY += 0.002f;
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
		Debug.Log (globalForceVector);
		rb.AddForce(globalForceVector);
	}
}
