using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonScript : MonoBehaviour {

	public void OnClickButtonClick()
	{
		Debug.Log ("onbuttonclick clickscript");
		TaskControllerSartScene.Instance.ClickOnClick ();
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}
}
