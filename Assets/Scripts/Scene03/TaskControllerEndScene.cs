using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskControllerEndScene : MonoBehaviourSingleton<TaskControllerEndScene> {

	//Menu Button Box
	public GameObject MenuBox;
	//Menu Button
	public GameObject MenuButtonPrefab;

	private MenuButtonScript _menuButtonScript;


	// Use this for initialization
	void Start () {
		PrepareEndScene ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrepareEndScene()
	{
		var MenuButtonGo = Instantiate (MenuButtonPrefab, MenuBox.transform);
		_menuButtonScript = MenuButtonGo.GetComponent<MenuButtonScript> ();
	}

	public void MenuButtonClick()
	{
		SceneManager.LoadScene ("Scene01");
	}
}
