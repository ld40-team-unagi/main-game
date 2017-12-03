using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {
	public GameObject plant;
	// Use this for initialization
	void Start () {
		//Instantiate (plant, transform.position + new Vector3(0f,0.5f,0f), transform.rotation);

		//GameObject prefab = (GameObject)Resources.Load("Prefabs/Effects/Prefab名");

		//Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
		// プレハブからインスタンスを生成
		plant = (GameObject)Instantiate(plant, transform.position + new Vector3(0f,0.4f,0f), Quaternion.identity);
		// 作成したオブジェクトを子として登録
		plant.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
