using UnityEngine;
using System.Collections;


public class Noise : MonoBehaviour {

	public Material mat;

	public void setSpeed(float value){
		mat.SetFloat ("baseSpeed",  value);
	}

	public void setScale(float value){
		mat.SetFloat ("noiseScale",  value);
	}

	// Use this for initialization
	void Start () {
		mat.SetFloat ("time",  0);
	}
	
	// Update is called once per frame
	void Update () {
		float delta = Time.deltaTime;
		mat.SetFloat ("time", (mat.GetFloat ("time") + delta));
		Debug.Log (mat.GetFloat("time"));
	}
}
