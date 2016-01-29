using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

	void Start () {
		GetComponent<AudioSource>().Play();
	}

	void Update () {
		if(!GetComponent<AudioSource>().isPlaying){
			Destroy(gameObject);
		}
	}
}
