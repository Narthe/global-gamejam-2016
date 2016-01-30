using UnityEngine;
using System.Collections;

public class MenuEffect : MonoBehaviour {
	public void CreateEffect(){
		Instantiate(Resources.Load("MenuSelectEffect"), transform.position, Quaternion.identity);
		Instantiate(Resources.Load("MenuSelectSound"));
	}
}
