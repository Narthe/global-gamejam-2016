using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public RectTransform _mainMenu;
	public RectTransform _creditsMenu;

	private string _currentMenu;
	private Vector3 _mainMenuStartPosition;
	private Vector3 _creditsMenuStartPosition;


	void Start () {
		_currentMenu = "main";

		_mainMenuStartPosition = _mainMenu.position;
		_creditsMenuStartPosition = _creditsMenu.position;
	}

	void Update () {
		if(_currentMenu == "credits"){
			EventSystem.current.SetSelectedGameObject(null);
			if(Input.GetButtonDown("Cancel")){
				Instantiate(Resources.Load("MenuCancelSound"));
				_currentMenu = "main";
			}

		} else {
			if(EventSystem.current.currentSelectedGameObject == null){
				EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
			}
		}

		ShowMenus();
	}

	public void SetMenu(string menu){
		Instantiate(Resources.Load("MenuValidateSound"));
		_currentMenu = menu;

	}

	public void Play(){
		if(_currentMenu == "main"){
			//Do stuff
			SceneManager.LoadScene(1);
		}
	}
		
	void ShowMenus(){
		if(_currentMenu == "main"){
			_mainMenu.position = Vector3.Lerp(_mainMenu.position, new Vector3(0, _mainMenuStartPosition.y, _mainMenuStartPosition.z), Time.deltaTime * 8.0f);
		} else {
			_mainMenu.position = Vector3.Lerp(_mainMenu.position, new Vector3(_mainMenuStartPosition.x, _mainMenuStartPosition.y, _mainMenuStartPosition.z), Time.deltaTime * 8.0f);
		}

		if(_currentMenu == "credits"){
			_creditsMenu.position = Vector3.Lerp(_creditsMenu.position, new Vector3(0, _creditsMenuStartPosition.y, _creditsMenuStartPosition.z), Time.deltaTime * 8.0f);
		} else {
			_creditsMenu.position = Vector3.Lerp(_creditsMenu.position, new Vector3(_creditsMenuStartPosition.x, _creditsMenuStartPosition.y, _creditsMenuStartPosition.z), Time.deltaTime * 8.0f);
		}
	}

	public void RageQuit(){
		Application.Quit();
	}
}
