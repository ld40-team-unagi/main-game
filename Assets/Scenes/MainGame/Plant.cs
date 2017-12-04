using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant: MonoBehaviour{
	public int maxAge = 1800;

	public int cropYields  = 100;
	public int seeds  = 2;

	public int deadAge = 4000;
	public int maxLife = 1000;

	public AudioClip potPut;
	public AudioClip potCut;
	public AudioClip potOk;
	public AudioClip potNg;

	public void Start(){
		SE.Play(gameObject, potPut);
		life = maxLife;
	}

	public void Update(){
		age += 1;
		if (IsDead()) {
			if(ngSE){
				SE.Play(gameObject, potNg);
				ngSE = false;
			}
			return;
		}

		if (transform.up.y < Mathf.Cos(angle)) {
			life -= 1;
		}

		if (CanCrop ()) {
			if(okSE){
				SE.Play(gameObject, potOk);
				okSE = false;
			}
			Animate ();
			return;
		}

		float s = ((float)age / (float)maxAge);
		transform.localScale = new Vector3 (s, s, s);
	}

	public bool Crop(){
		if(CanCrop()){
			SE.Play(gameObject, potCut);
			life = 0;
			return true;
		}
		return false;
	}

	public int CurrentAge(){
		return age;
	}

	public bool CanCrop(){
		return age >= maxAge && !IsDead();
	}

	public bool IsDead(){
		if(life <= 0){
			return true;
		}
		if(age >= deadAge){
			return true;
		}
		return false;
	}

	int age = 0;
	float animationTimer = 0;
	float angle = 45;
	int life;
	bool cutSE = true;
	bool ngSE = true;
	bool okSE = true;

	void Animate(){
		transform.Rotate(new Vector3 (0, 1, 0));
		if (animationTimer > 1f)return;
		float scale = EaseOutElastic (0f, 1f, animationTimer);
		transform.localScale = new Vector3 (scale, scale, scale);
		animationTimer += 0.02f;
	}

	float EaseOutElastic(float start, float end, float value)
	{
		end -= start;

		float d = 1f;
		float p = d * .3f;
		float s;
		float a = 0;

		if (value == 0) return start;

		if ((value /= d) == 1) return start + end;

		if (a == 0f || a < Mathf.Abs(end))
		{
			a = end;
			s = p * 0.25f;
		}
		else
		{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}

		return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
	}

	void DestroyPlant(int plantlife){
		if (transform.up.y < Mathf.Cos(angle)) {
			maxLife -= 1;

			if(maxLife <= 0){
				Destroy (gameObject);
			}
		}
	}
}
