using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {

	private float _timer;

	void Start(){
		_timer = 0;
	}

	void Update () {
		_timer += Time.deltaTime;

		if(_timer > 25 || Input.GetButtonDown("Submit")){
			SceneManager.LoadScene(0);			
		}

		transform.Translate(0, 1, 0);
	}
}
