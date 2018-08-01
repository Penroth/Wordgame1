using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour {


	public void GetInputName(string ID)
	{
		TaskControllerStartScene.Instance.IDInput = ID;
		Debug.Log (ID);
	}
		
	public void GetInputAge(string ageString)
	{
		int age = int.Parse (ageString);
		TaskControllerStartScene.Instance.ageInput = age;
		Debug.Log (age);
	}

	public void GetInputClass(string classLevelString)
	{
		int classLevel = int.Parse (classLevelString);
		TaskControllerStartScene.Instance.classInput = classLevel;
		Debug.Log (classLevel);
	}
}