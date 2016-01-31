using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	private Vector3 position;

	private float speedX;
	private float speedY;

	private float fadeFactor;
	private float rotateFactor;

	private Vector3 color;

	// Use this for initialization
	void Start () {
		float startX = Random.Range(0, Camera.main.pixelWidth);
		float startY = -150;

		position = Camera.main.ScreenToWorldPoint(new Vector3(startX, startY, 20));
		transform.position = position;

		speedX = Random.Range(0.1f,1f);
		speedY = Random.Range(0.5f,0.6f);

		fadeFactor = Random.Range(0.1f, 0.5f);
		rotateFactor = Random.Range(-0.1f, 0.1f);

		//color = GameObject.Find ("GameControl").GetComponent<GameControl>().customColor;
		//gameObject.transform.GetComponent<Renderer>().material.color = new Vector4(color.x,color.y, color.z, 1);
	}
	
	// Update is called once per frame
	void Update () {
		float xVel = -1 * speedX * Time.deltaTime;
		float yVel = 1 * speedY * Time.deltaTime;

		position = new Vector3(position.x + xVel, position.y + yVel, 10);
		transform.position = position;

		transform.Rotate(0,0,rotateFactor);

		float alpha = gameObject.transform.GetComponent<Renderer>().material.color.a;
		alpha -= fadeFactor * Time.deltaTime;
		gameObject.transform.GetComponent<Renderer>().material.color = new Vector4(1,1,1, alpha);

		if(alpha < 0){
			Destroy(gameObject);
		}
	}
}
