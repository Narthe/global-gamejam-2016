using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.2f), Random.Range(0.8f, 1f), 0, 1f);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
