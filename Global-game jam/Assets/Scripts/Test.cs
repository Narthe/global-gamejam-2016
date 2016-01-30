using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		print("Horizontal : " + Input.GetAxis("Horizontal"));
		print("Vertical : " + Input.GetAxis("Vertical"));
	}
}
