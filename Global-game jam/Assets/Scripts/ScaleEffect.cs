using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {
	
	public bool Effect; // true = out, false = in
	public float FactorValue;

	private float _scaleValue;

	void Start () {	
		_scaleValue = transform.localScale.x;
	}

	void Update () {
		//Fade out ?
		if(Effect){
			if(_scaleValue > 0){
				_scaleValue -= FactorValue;
			} else {
				_scaleValue = 0;
			}
		} else {
			_scaleValue += FactorValue;
		}

		//Gfx update
		transform.localScale = new Vector2(_scaleValue, _scaleValue);
	}
}
