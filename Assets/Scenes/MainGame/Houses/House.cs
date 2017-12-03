using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
	public float maxRoofHeight = 20f;
	float roofHeight = 0f;
	float animationCounter = 0f;
	GameObject roof;

	// Use this for initialization
	void Start () {
		roof = transform.FindChild ("Roof").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasPlayer) {
			animationCounter = Mathf.Clamp01 (animationCounter + 0.05f);
			roofHeight = EaseOutElastic (0f, 1f, animationCounter)*maxRoofHeight;
		} else {
			animationCounter = Mathf.Clamp01 (animationCounter - 0.05f);
			roofHeight = EaseInBounce (0f, 1f, animationCounter)*maxRoofHeight;
		}

		roof.transform.position = transform.position + new Vector3(0f,roofHeight,0f);


		Debug.Log (hasPlayer);
		hasPlayer = false;
	}

	bool hasPlayer = false;

	void OnTriggerStay(Collider c){
		GameObject target = c.gameObject;
		if (target.GetComponent<Player> () != null) {
			hasPlayer = true;
		}
	}
		
	public static float EaseInElastic(float start, float end, float value)
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
			s = p / 4;
		}
		else
		{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}

		return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
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

	public static float EaseInBounce(float start, float end, float value)
	{
		end -= start;
		float d = 1f;
		return end - EaseOutBounce(0, end, d - value) + start;
	}


	public static float EaseOutBounce(float start, float end, float value)
	{
		value /= 1f;
		end -= start;
		if (value < (1 / 2.75f))
		{
			return end * (7.5625f * value * value) + start;
		}
		else if (value < (2 / 2.75f))
		{
			value -= (1.5f / 2.75f);
			return end * (7.5625f * (value) * value + .75f) + start;
		}
		else if (value < (2.5 / 2.75))
		{
			value -= (2.25f / 2.75f);
			return end * (7.5625f * (value) * value + .9375f) + start;
		}
		else
		{
			value -= (2.625f / 2.75f);
			return end * (7.5625f * (value) * value + .984375f) + start;
		}
	}

}
