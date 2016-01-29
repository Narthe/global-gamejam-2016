using UnityEngine;
using System.Collections;

public class AlphaEffect : MonoBehaviour {

	public bool effect; // true = out, false = in
	public float factorValue;

	private float alphaValue;

	void Start () {	
		if(effect){
			alphaValue = 1.0f;
		} else {
			alphaValue = 0.0f;
		}
	}

	void Update () {
		//Fade out ?
		if(effect){
			if(alphaValue > 0){
				alphaValue -= factorValue;
			} else {
				Destroy(gameObject);
			}
		} else {
			if(alphaValue < 1){
				alphaValue += factorValue;
			} else {
				Destroy(gameObject);
			}
		}

		//Gfx update
		GetComponent<SpriteRenderer>().color = new Color(1,1,1, alphaValue);
	}
}
