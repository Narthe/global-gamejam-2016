using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public RectTransform _mainMenu;
	public RectTransform _creditsMenu;
	public RectTransform _controlsMenu;

	private string _currentMenu;
	private Vector3 _mainMenuStartPosition;
	private Vector3 _creditsMenuStartPosition;
	private Vector3 _controlsMenuStartPosition;

	private float _delayInput;


	void Start () {

		_delayInput = 0;

		_currentMenu = "main";

		_mainMenuStartPosition = _mainMenu.position;
		_creditsMenuStartPosition = _creditsMenu.position;
		_controlsMenuStartPosition = _controlsMenu.position;
	}

	void Update () {
		_delayInput += Time.deltaTime;

		if(_currentMenu == "credits"){
			EventSystem.current.SetSelectedGameObject(null);
			if(Input.GetButtonDown("Cancel")){
				Instantiate(Resources.Load("MenuCancelSound"));
				_currentMenu = "main";
			}

		} else if(_currentMenu == "main"){
			if(EventSystem.current.currentSelectedGameObject == null){
				EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
			}
		} else if(_currentMenu == "controls"){
			EventSystem.current.SetSelectedGameObject(null);		
			if(Input.GetButtonDown("Submit") && _delayInput > 1.0f){
				Play();
			}
		} else if(_currentMenu == "GO"){
			if(_delayInput > 3){
				SceneManager.LoadScene(1);
			}
		}

		ShowMenus();
	}

	public void SetMenu(string menu){
		Instantiate(Resources.Load("MenuValidateSound"));
		_currentMenu = menu;

	}

	public void ShowControls(){
		if (_currentMenu == "main"){
			_currentMenu = "controls";
			_delayInput = 0;
		}
	}

	public void Play(){
		if(_currentMenu == "controls"){
			//Do stuff
			_currentMenu = "GO";
			Destroy(GameObject.Find("SmokeGenerator"));
			Destroy(GameObject.Find("GameTitle"));
			_delayInput = 0;
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

		if(_currentMenu == "controls"){
			_controlsMenu.position = Vector3.Lerp(_controlsMenu.position, new Vector3(0, _controlsMenuStartPosition.y, _controlsMenuStartPosition.z), Time.deltaTime * 8.0f);
		} else {
			_controlsMenu.position = Vector3.Lerp(_controlsMenu.position, new Vector3(_controlsMenuStartPosition.x, _controlsMenuStartPosition.y, _controlsMenuStartPosition.z), Time.deltaTime * 8.0f);
		}
	}

	public void RageQuit(){
		Application.Quit();
	}
}
