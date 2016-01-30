using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public Transform _mainMenu;
	public Transform _creditsMenu;

	private string _currentMenu;

	void Start () {
		_currentMenu = "main";
	}

	void Update () {
		if(EventSystem.current.currentSelectedGameObject == null){
			EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
		}
		
	}

	public void SetMenu(string menu){
		_currentMenu = menu;
	}

	public void RageQuit(){
		Application.Quit();
	}
}
