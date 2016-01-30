using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {
	
	public bool Effect; // true = out, false = in
	public float FactorValue;

	private float _scaleValue;

	private float _trueScaleX;
	private float _trueScaleY;

	void Start () {	
		_scaleValue = 0;

		_trueScaleX = transform.localScale.x;
		_trueScaleY = transform.localScale.y;
	}

	void Update () {
		//Fade out ?
		if(Effect){
			if(_scaleValue > 0){
				_scaleValue -= FactorValue * Time.deltaTime;
			} else {
				_scaleValue = 0;
			}
		} else {
			_scaleValue += FactorValue * Time.deltaTime;
		}

		//Gfx update
		transform.localScale = new Vector2(_trueScaleX + _scaleValue, _trueScaleY + _scaleValue);
	}
}
