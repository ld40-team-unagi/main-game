using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Transform target;

	public float counterForLostInit = 300f;
	public float searchingFov = 60f;
	public float searchingDistance = 50f;
	public float walkingSpeed = 3.5f;
	public float walkingAcceleration = 8f;
	public float runningSpeed = 4f;
	public float runningAcceleration = 12f;

	NavMeshAgent agent;
	float counterForLost;

	void Start(){
		counterForLost = counterForLostInit;
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		Search ();

		if (isRandomWalking) {
			RandomWalk ();
		} else {
			Chase ();
		}

	}

	void changeModeToRunning(){
		isRandomWalking = false;
		agent.speed = runningSpeed;
		agent.acceleration = runningAcceleration;
		agent.angularSpeed = 360f;
	}

	void changeModeToWalking(){
		isRandomWalking = true;
		arrived = true;
		agent.speed = walkingSpeed;
		agent.acceleration = walkingAcceleration;
		agent.angularSpeed = agent.speed * 34f;
	}

	void Search(){
		if (Find (target.position, searchingFov * Mathf.PI / 180f,searchingDistance)) {
			counterForLost = counterForLostInit;
			changeModeToRunning ();
		} else {
			counterForLost -= 1f;
		}
	}

	bool Find(Vector3 position, float range,float distance){
		NavMeshHit hit;
		if (agent.Raycast (position, out hit))
			return false;
		if (hit.distance > distance)
			return false;
		Vector3 normal = (hit.position - transform.position).normalized;
		Vector3 localNormal = Quaternion.Inverse (transform.rotation) * normal;
		if (localNormal.z <= 0)return false;
		float r = Mathf.Atan2 (localNormal.x,localNormal.z);
		if (Mathf.Abs (r) < range * 0.5)
			return true;
		return true;
	}

	void Chase(){
		if (counterForLost <= 0f) {
			agent.destination = transform.position; 
			changeModeToWalking ();
		} else {
			agent.destination = target.position;
		}

	} 

	bool isRandomWalking = true;
	Vector3 randomWalkTarget;
	bool arrived = true;
	void RandomWalk(){
		if (arrived) {
			bool isReachable;
			do {
				Vector3 direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
				randomWalkTarget = direction * Random.Range (1f, 10f);
				isReachable = !NavMesh.CalculatePath (transform.position, randomWalkTarget, NavMesh.AllAreas, new NavMeshPath());
			} while(!isReachable);
			agent.destination = randomWalkTarget;
			arrived = false;
		
		} else {
			if (agent.velocity.magnitude == 0f) {
				arrived = true;
			}
		}
			
	}

}