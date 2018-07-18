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

	//box for Righthanded Checkbox
	public GameObject RightHandedBox;
	//box for Righthanded Checkbox
	public GameObject RightHanded;
	//box for Lefthanded Checkbox
	public GameObject LeftHandedBox;
	//left handed checkbox
	public GameObject LeftHanded;
	//box for Gender Dropdown
	public GameObject GenderBox;
	//Gender Dropdown
	public GameObject GenderDropDown;
	//name input field box
	public GameObject NameBox;
	//name input field
	public GameObject NameField;
	//Age Input Box
	public GameObject AgeBox;
	//Age input field
	public GameObject AgeField;
	//Misc input Box
	public GameObject MiscBox;
	//misc input field
	public GameObject MiscField;



	private ClickButtonScript _clickButtonScript;
	private DragButtonScript _dragButtonScript;
	private GenderDropDownScript _genderDropScript;
	private RightAndLeftHandedScript _RightLeftScript;
	private InputFieldScript _InputScript;

	// Use this for initialization
	void Start () {
		PrepareStartScene ();
	}
	


	public void PrepareStartScene()
	{
		var ClickButtonGo = Instantiate (ClickButtonPrefab, ClickBox.transform);
		_clickButtonScript = ClickButtonGo.GetComponent<ClickButtonScript> ();
		_clickButtonScript.SetInteractable (false);

		var DragButtonGo = Instantiate (DragButtonPrefab, DragBox.transform);
		_dragButtonScript = DragButtonGo.GetComponent<DragButtonScript> ();
		_dragButtonScript.SetInteractable (false);

		var GenderDropGo = Instantiate (GenderDropDown, GenderBox.transform);
		_genderDropScript = GenderDropGo.GetComponent<GenderDropDownScript> ();

		var RightGo = Instantiate (RightHanded, RightHandedBox.transform);
		_RightLeftScript = RightGo.GetComponent<RightAndLeftHandedScript> ();

		var LeftGo = Instantiate (LeftHanded, LeftHandedBox.transform);
		_RightLeftScript = LeftGo.GetComponent<RightAndLeftHandedScript> ();

		var NameGo = Instantiate (NameField, NameBox.transform);
		_InputScript = NameGo.GetComponent<InputFieldScript> ();;

		var AgeGo = Instantiate (AgeField, AgeBox.transform);
		_InputScript = AgeGo.GetComponent<InputFieldScript> ();;

		var MiscGo = Instantiate (MiscField, MiscBox.transform);
		_InputScript = MiscGo.GetComponent<InputFieldScript> ();


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
