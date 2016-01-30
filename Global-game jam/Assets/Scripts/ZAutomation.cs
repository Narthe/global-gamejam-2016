using UnityEngine;
using System.Collections;

public class ZAutomation : MonoBehaviour {
	
	void Start () {
		foreach(GameObject victim in GameObject.FindGameObjectsWithTag("ZAutomated")){
			victim.GetComponent<SpriteRenderer>().sortingOrder = (int)(victim.transform.position.y * -100)	;
		}
	}
	

}
