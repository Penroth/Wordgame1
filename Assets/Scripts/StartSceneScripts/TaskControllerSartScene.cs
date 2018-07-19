using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TaskControllerSartScene : MonoBehaviourSingleton<TaskControllerSartScene> {
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
	private HandedScript _handedScript;
	private InputFieldScript _inputScript;


	public string nameInput = "";
	public int ageInput = 0;
	public string miscInput = "";
	public string handed = "Rechsthänder";
	public string gender = "Männlich";


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

		var NameGo = Instantiate (NameField, NameBox.transform);
		_inputScript = NameGo.GetComponent<InputFieldScript> ();;

		var AgeGo = Instantiate (AgeField, AgeBox.transform);
		_inputScript = AgeGo.GetComponent<InputFieldScript> ();;

		var MiscGo = Instantiate (MiscField, MiscBox.transform);
		_inputScript = MiscGo.GetComponent<InputFieldScript> ();

		Debug.Log (handed + "in preparescene");
		Debug.Log (gender + "gender in preparescene");

	
	}

	public void checkIfNothingIsEmpty()
	{
		if (nameInput != "" && miscInput != "" && ageInput != 0) 
		{
			_clickButtonScript.SetInteractable (true);
			_dragButtonScript.SetInteractable (true);
		}
	}

	public void HandedSwitch()
	{
		string hand = _handedScript.GetComponent<Dropdown> ().captionText.text.ToString ();
//		_handedScript.GetComponent<Dropdown> ().onValueChanged.AddListener (delegate {
//			_handedScript.GetComponent<Dropdown> ().captionText.ToString ();
//		});
		//hand = _handedScript.GetComponent<Dropdown> ().captionText.ToString ();
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
		string filePath = Application.persistentDataPath + "/"+ nameInput + clickOrDrag + miscInput + ".csv";
		StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8);
		string firstLine = "Korrektes Wort; Eingegebenes Wort; Datum" + "; " + "Alter: " + ageInput.ToString () + "; " + handed + "; " + gender;
		writer.WriteLine(firstLine);
		writer.Close();
	}

	public void ClickOnClick()
	{
		Debug.Log ("click in task");
		CreateFile ("Click");
		SceneManager.LoadScene ("ClickScene");
	}

	public void DragOnClick()
	{
		Debug.Log ("drag in task");
		CreateFile ("Drag");
		SceneManager.LoadScene ("DragScene");
	}
}
