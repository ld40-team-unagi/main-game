using UnityEngine;
using System.Collections;
using UnityEngine.AI;

enum Status{
	Walking,
	Chasing,
	Cropping
}

public class Enemy : MonoBehaviour {

	public GameObject target;

	public float counterForLostInit = 300f;
	public float searchingFov = 60f;
	public float searchingDistance = 50f;
	public float walkingSpeed = 3.5f;
	public float walkingAcceleration = 8f;
	public float runningSpeed = 4f;
	public float runningAcceleration = 12f;

	public AudioClip walkingSoundClip;
	public AudioClip runningSoundClip;

	AudioSource audioSource;

	NavMeshAgent agent;
	float counterForLost;

	Status status = Status.Walking;



	void Start(){
		counterForLost = counterForLostInit;
		agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource> ();
		changeModeToWalking ();
	}

	void Update () {
		if (target.GetComponent<Player> ().isProduced) {
			Search ();
		}

		switch (status) {
		case Status.Walking:
			RandomWalk ();
			return;
		case Status.Chasing:
			Chase ();
			return;
		case Status.Cropping:
			Crop ();
			return;
		default:
			return;
		}
	}

	void changeModeToCropping(){
		status = Status.Cropping;
		agent.speed = walkingSpeed;
		agent.acceleration = walkingAcceleration;
		agent.angularSpeed = agent.speed * 34f;
		audioSource.clip = walkingSoundClip;
		audioSource.Play();
	}

	void changeModeToChasing(){
		status = Status.Chasing;
		agent.speed = runningSpeed;
		agent.acceleration = runningAcceleration;
		agent.angularSpeed = 360f;
		audioSource.clip = runningSoundClip;
		audioSource.Play();
	}

	void changeModeToWalking(){
		status = Status.Walking;
		arrived = true;
		agent.speed = walkingSpeed;
		agent.acceleration = walkingAcceleration;
		agent.angularSpeed = agent.speed * 34f;
		audioSource.clip = runningSoundClip;
		audioSource.Play();
	}

	GameObject cropTarget;

	void Crop(){
		if (cropTarget != null) {
			changeModeToWalking ();
			return;
		}

	}

	void Search(){
		if (Find (target.transform.position, searchingFov * Mathf.PI / 180f,searchingDistance)) {
			counterForLost = counterForLostInit;
			changeModeToChasing ();
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
			agent.destination = target.transform.position;
		}

	} 


	Vector3 randomWalkTarget;
	bool arrived = true;
	long randomWarkingTime;
	void RandomWalk(){
		if (arrived) {
			bool isReachable;
			do {
				Vector3 direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
				randomWalkTarget = transform.position + direction * Random.Range (1f, 10f);
				isReachable = !NavMesh.CalculatePath (transform.position, randomWalkTarget, NavMesh.AllAreas, new NavMeshPath());
			} while(!isReachable);
			agent.destination = randomWalkTarget;
			arrived = false;
			randomWarkingTime = 0;
		
		} else {
			
			if (randomWarkingTime > 60 &&agent.velocity.magnitude < 0.1f) {
				arrived = true;
			}
			randomWarkingTime++;
		}
			
	}

	void OnTriggerStay(Collider c){
		GameObject target = c.gameObject;
		if (target.GetComponent<Pot> () != null) {
			if (status != Status.Walking)
				return;
			OnTriggerStayWithPot (c);
		}
	}

	void OnTriggerStayWithPot(Collider c){
		

		GameObject pot = c.gameObject;
		if (pot.GetComponent<Pot> ().IsLying())
			return;
		agent.destination = pot.transform.position;
		changeModeToCropping ();
		cropTarget = pot;

		if (Vector3.Distance (pot.transform.position, transform.position) < 1f) {
			Vector3 direction = (Vector3.Scale((pot.transform.position - transform.position),new Vector3 (1f, 0f, 1f))+ new Vector3(Random.Range(-1f,1f),0f,Random.Range(-1f,1f))).normalized + new Vector3(0f,40f,0f);
			pot.GetComponent<Rigidbody> ().AddForceAtPosition (direction*2f, pot.transform.position + new Vector3 (0f, 0.5f, 0f));
		}
	}

}