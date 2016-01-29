using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform startingTarget;
	public float delayTracking;

	public Transform firstDelimiter;
	public Transform secondDelimiter;

	//Object to follow
	private Transform target;

	//Bounds
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	//Camera position
	private float xPos;
	private float yPos;
	private float zPos;

	void Start () {
		target = startingTarget;

		minX = firstDelimiter.position.x;
		maxX = secondDelimiter.position.x;

		minY = firstDelimiter.position.y;
		maxY = secondDelimiter.position.y;

		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		//Define new position
		xPos = target.position.x;
		yPos = target.position.y;

		//Check if target is out of bounds
		//X-axis
		if(xPos < minX){
			xPos = minX;
		}
		if(xPos > maxX){
			xPos = maxX;
		}

		//Y-axis
		if(yPos < minY){
			yPos = minY;
		}
		if(yPos > maxY){
			yPos = maxY;
		}

		Vector3 newPosition = new Vector3(xPos, yPos, zPos);

		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * delayTracking);
	}

	public void RedefineTarget(Transform target){
		this.target = target;
	}

	public void RedefineTarget(string targetName){
		this.target = GameObject.Find(targetName).transform;
	}
}
