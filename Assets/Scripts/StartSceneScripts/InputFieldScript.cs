using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour {


	public void GetInputName(string name)
	{
		TaskControllerStartScene.nameInput = name;
		Debug.Log (name);
	}
		
	public void GetInputAge(string ageString)
	{
		int age = int.Parse (ageString);
		TaskControllerStartScene.Instance.ageInput = age;
		Debug.Log (age);
	}

	public void GetInputMisc(string misc)
	{
		TaskControllerStartScene.miscInput = misc;
		Debug.Log (misc);
	}
}