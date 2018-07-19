using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour {


	public void GetInputName(string name)
	{
		TaskControllerSartScene.Instance.nameInput = name;
		Debug.Log (name);
	}
		
	public void GetInputAge(string ageString)
	{
		int age = int.Parse (ageString);
		TaskControllerSartScene.Instance.ageInput = age;
		Debug.Log (age);
	}

	public void GetInputMisc(string misc)
	{
		TaskControllerSartScene.Instance.miscInput = misc;
		Debug.Log (misc);
	}
}