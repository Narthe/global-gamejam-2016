using UnityEngine;
using System.Collections;

public class AlphaEffect : MonoBehaviour {

	public bool Effect; // true = out, false = in
	public float FactorValue;

	private float _alphaValue;

	void Start () {	
		if(Effect){
			_alphaValue = 1.0f;
		} else {
			_alphaValue = 0.0f;
		}
	}

	void Update () {
		//Fade out ?
		if(Effect){
			if(_alphaValue > 0){
				_alphaValue -= FactorValue;
			} else {
				Destroy(gameObject);
			}
		} else {
			if(_alphaValue < 1){
				_alphaValue += FactorValue;
			} else {
				Destroy(gameObject);
			}
		}

		//Gfx update
		GetComponent<SpriteRenderer>().color = new Color(1,1,1, _alphaValue);
	}
}
