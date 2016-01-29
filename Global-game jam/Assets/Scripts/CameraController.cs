using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform StartingTarget;
	public float DelayTracking;

	public Transform FirstDelimiter;
	public Transform SecondDelimiter;

	//Object to follow
	private Transform _target;

	//Bounds
	private float _minX;
	private float _maxX;
	private float _minY;
	private float _maxY;

	//Camera position
	private float _xPos;
	private float _yPos;
	private float _zPos;

	void Start () {
		_target = StartingTarget;

		_minX = FirstDelimiter.position.x;
		_maxX = SecondDelimiter.position.x;

		_minY = FirstDelimiter.position.y;
		_maxY = SecondDelimiter.position.y;

		_xPos = transform.position.x;
		_yPos = transform.position.y;
		_zPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		//Define new position
		_xPos = _target.position.x;
		_yPos = _target.position.y;

		//Check if target is out of bounds
		//X-axis
		if(_xPos < _minX){
			_xPos = _minX;
		}
		if(_xPos > _maxX){
			_xPos = _maxX;
		}

		//Y-axis
		if(_yPos < _minY){
			_yPos = _minY;
		}
		if(_yPos > _maxY){
			_yPos = _maxY;
		}

		Vector3 newPosition = new Vector3(_xPos, _yPos, _zPos);

		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * DelayTracking);
	}

	public void RedefineTarget(Transform target){
		_target = target;
	}

	public void RedefineTarget(string targetName){
		_target = GameObject.Find(targetName).transform;
	}
}
