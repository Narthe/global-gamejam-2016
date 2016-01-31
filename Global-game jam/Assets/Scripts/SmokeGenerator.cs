using UnityEngine;
using System.Collections;

public class SmokeGenerator : MonoBehaviour {

	public float minIntervalTime = 0.1f;
	public float maxIntervalTime = 0.2f;
	
	private float timer;
	private float intervalTime;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		intervalTime = Random.Range(minIntervalTime, maxIntervalTime);

		if(timer > intervalTime){
			timer = 0;
			string[] smoke = new string[2]{"Smoke1Entity", "Smoke2Entity"};

			Instantiate(Resources.Load (smoke[Random.Range (0, smoke.Length)] ) );
		} else {
			timer += Time.deltaTime;
		}
	}
}
