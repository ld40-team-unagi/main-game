using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant: MonoBehaviour{
	public int maxAge = 1800;
	public int cropYields  = 100;
	public int seeds  = 2;
	public int deadAge = 4000;

	public void Update(){
		age += 1;
		if (IsDead()) {
			return;
		}
		if (CanCrop ()) {
			Animate ();
			return;
		}
		float s = ((float)age / (float)maxAge);
		transform.localScale = new Vector3 (s, s, s);
	}

	public int CurrentAge(){
		return age;
	}

	public bool CanCrop(){
		return age >= maxAge && !IsDead();
	}
	public bool IsDead(){
		return age >= deadAge;
	}
	int age = 0;
	float animationTimer = 0;

	void Animate(){
		transform.eulerAngles = transform.eulerAngles + new Vector3 (0f, 0.8f, 0f);
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
}
