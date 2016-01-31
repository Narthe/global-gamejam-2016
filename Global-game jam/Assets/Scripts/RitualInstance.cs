using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RitualInstance : MonoBehaviour {

	[HideInInspector] public bool RitualSuccess;
	[HideInInspector] public bool RitualTerminated;

	public GameObject KeyTemoin;
	public GameObject RitualBack;
	public GameObject BackPartition;
	public GameObject BackPartition2;
	public GameObject KeyManager;

	private float _alphaValue;
	private int _status; //0 = starting, 1 = started, 2 = terminating, 3 = terminator LOELELEOEPKZAELKQSMDK
	private float _delayTerminating;

	// Use this for initialization
	void Start () {
		//For MASTER
		RitualSuccess = false;
		RitualTerminated = false;

		//Alpha to zero
		_alphaValue = 0;

		//Init status
		_status = 0;

		_delayTerminating = 0;

		UpdateAlpha();

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y, -5);

		if(_status == 0){
			if(_alphaValue < 1){
				_alphaValue += Time.deltaTime * 1.1f;
			} else {
				KeyManager.GetComponent<KeyManager>().Init();
				RitualBack.GetComponent<RitualBackEffect>().Init();
				_alphaValue = 1;
				_status = 1;
			}
			UpdateAlpha();
		} else if(_status == 1){
			if(KeyManager.GetComponent<KeyManager>().GetStatus()){
				RitualBack.GetComponent<RitualBackEffect>().Stop();
				if(KeyManager.GetComponent<KeyManager>().GetSuccess()){
					RitualSuccess = true;
				} else {
					RitualSuccess = false;
				}
				_delayTerminating += Time.deltaTime;
				if(_delayTerminating > 2.0f){
					_status = 2;
				}
			}
		} else if(_status == 2){
			if(_alphaValue > 0){
				_alphaValue -= Time.deltaTime * 1.1f;
			} else {
				Destroy(KeyManager);
				Destroy(RitualBack);
				_alphaValue = 0;
				_status = 3;
				RitualTerminated = true; //Now the master should get hand back.
			}
			UpdateAlpha();
		}
	}

	void UpdateAlpha(){
		Color tempColor;

		tempColor = KeyTemoin.GetComponent<SpriteRenderer>().color;
		KeyTemoin.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, _alphaValue);

		tempColor = RitualBack.GetComponent<SpriteRenderer>().color;
		RitualBack.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, _alphaValue);

		if(_alphaValue < 0.5f){
			tempColor = BackPartition.GetComponent<SpriteRenderer>().color;
			BackPartition.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, _alphaValue);

			tempColor = BackPartition2.GetComponent<SpriteRenderer>().color;
			BackPartition2.GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, _alphaValue);
		}
	}
}
