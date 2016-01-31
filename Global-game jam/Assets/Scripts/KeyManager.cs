using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour {

	public Key[] Keys;
	public Transform KeyTemoin;

	private float _timer;
	private int _index;
	private int _score;

	private bool _success;
	private bool _started;
	private bool _terminated;

	// Use this for initialization
	void Start () {

		_terminated = false;
		_success = false;
		_started = false;

		_timer = 0;
		_index = 0;
		_score = 0;

		float timeline = 0;
		for(int i = 0; i < Keys.Length; i++){
			timeline += Keys[i].Time;
			Keys[i].Time = timeline;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(_started && !_terminated){
			_timer += Time.deltaTime;

			if(_index < Keys.Length){
				if(_timer > Keys[_index].Time){
					GameObject keyInstance = (GameObject)Instantiate(Resources.Load("KeyInstance"), transform.position, Quaternion.identity);
					keyInstance.GetComponent<KeyInstance>().Name = Keys[_index].Name;
					keyInstance.GetComponent<KeyInstance>().Target = KeyTemoin;
					keyInstance.GetComponent<KeyInstance>().Manager = this;
					_index ++;
				}
			} else {
				if(_timer > Keys[_index - 1].Time + 2.0f){
					if(_score == Keys.Length){
						_success = true;
						Instantiate(Resources.Load("RitualSuccessSound"));
					} else {
						Instantiate(Resources.Load("RitualFailSound"));
					}
					_terminated = true;
				}
			}
		}
	}

	public bool GetStatus(){
		return _terminated;
	}

	public bool GetSuccess(){
		return _success;
	}

	public void Scored(string name){
		Instantiate(Resources.Load("KeySuccess"), KeyTemoin.position, Quaternion.identity);
		GameObject g = Instantiate(Resources.Load("RitualSuccessKeySound")) as GameObject;
	    float pitchValue = 0f;
        switch (name)
	    {
            case "Left":
	            pitchValue = .5f;
                break;
            case "Right":
                pitchValue = -.5f;
                break;
            case "Up":
                pitchValue = 1f;
                break;
            case "Down":
                pitchValue = -1f;
                break;


        }
	    g.GetComponent<AudioSource>().pitch += pitchValue;

        _score ++;
	}

	public void Fail(){
		Instantiate(Resources.Load("KeyFail"), KeyTemoin.position, Quaternion.identity);
		Instantiate(Resources.Load("RitualFailKeySound"));
	}

	public void Init(){
		_started = true;
	}
}
