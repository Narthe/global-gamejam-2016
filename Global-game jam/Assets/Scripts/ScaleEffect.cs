using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {
	
	public bool effect; // true = out, false = in
	public float factorValue;

	private float scaleValue;

	void Start () {	
		scaleValue = transform.localScale.x;
	}

	void Update () {
		//Fade out ?
		if(effect){
			if(scaleValue > 0){
				scaleValue -= factorValue;
			} else {
				scaleValue = 0;
			}
		} else {
			scaleValue += factorValue;
		}

		//Gfx update
		transform.localScale = new Vector2(scaleValue, scaleValue);
	}
}
