using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TaskControllerStartScene : MonoBehaviourSingleton<TaskControllerStartScene> {
	//Click Box
	public GameObject ClickBox;
	//Click Buttom
	public GameObject ClickButtonPrefab;
	//Drag and Drop Box
	public GameObject DragBox;
	//Drag and Drop Button
	public GameObject DragButtonPrefab;
	//Left- or Righthanded Box
	public GameObject HandedBox;
	//Left- or Righthanded Dropdown
	public Dropdown HandedPrefab;
	//box for Gender Dropdown
	public GameObject GenderBox;
	//Gender Dropdown
	public Dropdown GenderPrefab;
	//ID input field box
	public GameObject IDBox;
	//ID input field
	public GameObject IDField;
	//Age Input Box
	public GameObject AgeBox;
	//Age input field
	public GameObject AgeField;
	//Class input Box
	public GameObject ClassBox;
	//class input field
	public GameObject ClassField;



	private ClickButtonScript _clickButtonScript;
	private DragButtonScript _dragButtonScript;
	private GenderDropDownScript _genderDropScript;
	private HandedScript _handedScript;
	private InputFieldScript _inputScript;


	public string IDInput = "";
	public int ageInput = 0;
	public int classInput = 0;
	public string handed = "Rechsthänder";
	public string gender = "Männlich";
	public static string filepath = "";


	// Use this for initialization
	void Start () {
		PrepareStartScene ();
	}
	
	void Update()
	{
		checkIfNothingIsEmpty ();
	}

	public void PrepareStartScene()
	{
		var IDFromScript = IDScript.ID;
		var ageFromScript = IDScript.age;
		var classFromScript = IDScript.classLevel;




		var ClickButtonGo = Instantiate (ClickButtonPrefab, ClickBox.transform);
		_clickButtonScript = ClickButtonGo.GetComponent<ClickButtonScript> ();
		_clickButtonScript.SetInteractable (false);

		var DragButtonGo = Instantiate (DragButtonPrefab, DragBox.transform);
		_dragButtonScript = DragButtonGo.GetComponent<DragButtonScript> ();
		_dragButtonScript.SetInteractable (false);

		var GenderDropGo = Instantiate (GenderPrefab, GenderBox.transform);
		_genderDropScript = GenderDropGo.GetComponent<GenderDropDownScript> ();

		var HandedGo = Instantiate (HandedPrefab, HandedBox.transform);
		_handedScript = HandedGo.GetComponent<HandedScript> ();

		var IDGo = Instantiate (IDField, IDBox.transform);
		_inputScript = IDGo.GetComponent<InputFieldScript> ();

		if (IDFromScript != "") 
		{
			IDInput = IDFromScript;
			_inputScript.GetComponent<InputField> ().text = IDInput;
		}

		var AgeGo = Instantiate (AgeField, AgeBox.transform);
		_inputScript = AgeGo.GetComponent<InputFieldScript> ();
		if (ageFromScript != 0) 
		{
			ageInput = ageFromScript;
			_inputScript.GetComponent<InputField> ().text = ageInput.ToString();
		}

		var ClassGo = Instantiate (ClassField, ClassBox.transform);
		_inputScript = ClassGo.GetComponent<InputFieldScript> ();
		if (classFromScript != 0) 
		{
			classInput = classFromScript;
			_inputScript.GetComponent<InputField>().text = classInput.ToString();
		}

		Debug.Log (handed + "in preparescene");
		Debug.Log (gender + "gender in preparescene");
	}

	public void checkIfNothingIsEmpty()
	{
		if (IDInput != "" && ageInput != 0 && classInput != 0) 
		{
			_clickButtonScript.SetInteractable (true);
			_dragButtonScript.SetInteractable (true);
		}
	}

	public void HandedSwitch()
	{
		string hand = _handedScript.GetComponent<Dropdown> ().captionText.text.ToString ();
		Debug.Log(hand);
		handed = hand;
		Debug.Log(handed + " welche Hand");
	}

	public void GenderSwitch ()
	{
		string genderChanged = _genderDropScript.GetComponent<Dropdown> ().captionText.text.ToString ();
		Debug.Log (genderChanged);
		gender = genderChanged;
		Debug.Log (gender);

	}

	public void CreateFile (string clickOrDrag)
	{
		string currentTime = DateTime.Now.ToString("yyyy_MM_dd hh_mm");
		filepath = Application.persistentDataPath + "/"+ currentTime + "_" + IDInput + "_" + clickOrDrag + ".csv";
		StreamWriter writer = new StreamWriter(filepath, true, Encoding.UTF8);
		string firstLine = "Korrektes Wort; Eingegebenes Wort; Timer; Zurück Anzahl;" + "Alter: " + ageInput.ToString () + ";" + handed + ";" + gender + ";" + "Klassenstufe: " + classInput.ToString ();
		writer.WriteLine(firstLine);
		writer.Close();
	}

	public void ClickOnClick()
	{
		IDScript.age = ageInput;
		IDScript.ID = IDInput;
		IDScript.classLevel = classInput;
		Debug.Log ("click in task");
		CreateFile ("Click");
		SceneManager.LoadScene ("ClickScene");
	}

	public void DragOnClick()
	{
		IDScript.age = ageInput;
		IDScript.ID = IDInput;
		IDScript.classLevel = classInput;
		Debug.Log ("drag in task");
		CreateFile ("Drag");
		SceneManager.LoadScene ("DragScene");
	}
}
