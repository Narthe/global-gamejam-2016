using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlphaEffect : MonoBehaviour {

	public bool Effect; // true = out, false = in
	public float FactorValue;

	private float _alphaValue;

	private Color _initColor;

    private SpriteRenderer _s;
    private Image _i;



    void Start () {	
		if(Effect){
			_alphaValue = 1.0f;
		} else {
			_alphaValue = 0.0f;
		}
        _s = GetComponent<SpriteRenderer>();
        _i = GetComponent<Image>();

        if (_s != null)
            _initColor = _s.color;

        if (_i != null)
            _initColor = _i.color;

    }

	void Update () {
		//Fade out ?
		if(Effect){
			if(_alphaValue > 0){
				_alphaValue -= FactorValue * Time.deltaTime;
			} else {
				Destroy(gameObject);
			}
		} else {
			if(_alphaValue < 1){
				_alphaValue += FactorValue * Time.deltaTime;
			} else {
				_alphaValue = 1;
			}
		}

        //Gfx update
	    
        if(_s != null)
            _s.color = new Color(_initColor.r,_initColor.g, _initColor.b, _alphaValue);

        
        if(_i != null)
            _i.color = new Color(_initColor.r, _initColor.g, _initColor.b, _alphaValue);
    }
}
