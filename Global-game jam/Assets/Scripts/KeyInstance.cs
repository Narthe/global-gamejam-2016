using UnityEngine;
using System.Collections;

public class KeyInstance : MonoBehaviour {

	public string Name;
	public Transform Target;
	public KeyManager Manager;

	private float timer;


	void Start () {
		timer = 0;

		switch(Name)
		{
			case "up":
				GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/arrowUp", typeof(Sprite)) as Sprite;
				break;
			case "down":
				GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/arrowDown", typeof(Sprite)) as Sprite;
				break;
			case "left":
				GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/arrowLeft", typeof(Sprite)) as Sprite;
				break;
			case "right":
				GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/arrowRight", typeof(Sprite)) as Sprite;
				break;
			default :
				goto case "up";
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		/*
		if(timer > 2){
			Destroy(gameObject);
		}*/

		Vector3 newPosition = new Vector3(transform.position.x - (8f * Time.deltaTime), transform.position.y, 0);

		transform.position = newPosition;
	}

	void OnTriggerStay2D(Collider2D hit){
		if(Input.GetKey(KeyCode.UpArrow) && Name == "up"){
			Manager.Scored();
			Destroy(gameObject);
		}
		if(Input.GetKey(KeyCode.DownArrow) && Name == "down"){
			Manager.Scored();
			Destroy(gameObject);
		}
		if(Input.GetKey(KeyCode.LeftArrow) && Name == "left"){
			Manager.Scored();
			Destroy(gameObject);
		}
		if(Input.GetKey(KeyCode.RightArrow) && Name == "right"){
			Manager.Scored();
			Destroy(gameObject);
		}
	}
}
