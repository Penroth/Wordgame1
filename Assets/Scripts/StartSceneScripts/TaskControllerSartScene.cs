using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskControllerSartScene : MonoBehaviourSingleton<TaskControllerSartScene> {
	//Click Box
	public GameObject ClickBox;
	//Click Buttom
	public GameObject ClickButtonPrefab;
	//Drag and Drop Box
	public GameObject DragBox;
	//Drag and Drop Button
	public GameObject DragButtonPrefab;

	private ClickButtonScript _clickButtonScript;
	private DragButtonScript _dragButtonScript;

	// Use this for initialization
	void Start () {
		PrepareStartScene ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrepareStartScene()
	{
		var ClickButtonGo = Instantiate (ClickButtonPrefab, ClickBox.transform);
		_clickButtonScript = ClickButtonGo.GetComponent<ClickButtonScript> ();

		var DragButtonGo = Instantiate (DragButtonPrefab, DragBox.transform);
		_dragButtonScript = DragButtonGo.GetComponent<DragButtonScript> ();
	}

	public void ClickOnClick()
	{
		Debug.Log ("click in task");
		SceneManager.LoadScene ("ClickScene");
	}

	public void DragOnClick()
	{
		Debug.Log ("drag in task");
		SceneManager.LoadScene ("DragScene");
	}
}
