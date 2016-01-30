using UnityEngine;
using System.Collections;

public class RitualBackEffect : MonoBehaviour {

	private float _timer;
	private bool _started;

	// Use this for initialization
	void Start () {
		_timer = 1.2f;
		_started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(_started){
			_timer += Time.deltaTime;

			if(_timer > 1.2f){
				_timer = 0;
				Instantiate(Resources.Load("RitualEffectHalo"));
			}
			transform.Rotate(0,0,-0.1f);
		}
	}

	public void Init(){
		_started = true;
	}

	public void Stop(){
		_started = false;
	}
}
