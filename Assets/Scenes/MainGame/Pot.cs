using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {
	public GameObject plant;
	public Plant plantScript;
	// Use this for initialization
	void Start () {
		//Instantiate (plant, transform.position + new Vector3(0f,0.5f,0f), transform.rotation);

		//GameObject prefab = (GameObject)Resources.Load("Prefabs/Effects/Prefab名");

		//Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
		// プレハブからインスタンスを生成
		plant = (GameObject)Instantiate(plant, transform.position + new Vector3(0f,0.4f,0f), Quaternion.identity);
		plantScript = plant.GetComponent<Plant>();
		// 作成したオブジェクトを子として登録
		plant.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(plantScript.IsDead()){
			deadAnimate();
		}
	}
	void deadAnimate(){
		int age = plantScript.CurrentAge();
		int deleteTime = plantScript.deadAge;
		float scale = age - deleteTime;
		scale /= 30f;
		scale = 1f - scale;
		if(scale <= 0f){
			Destroy(gameObject);
			return;
		}
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
