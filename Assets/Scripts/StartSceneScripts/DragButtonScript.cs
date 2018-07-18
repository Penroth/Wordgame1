using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragButtonScript : MonoBehaviour {

	public void OnDragButtonClick()
	{
		Debug.Log ("onbuttonclick dragscript");
		TaskControllerSartScene.Instance.DragOnClick ();
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}
}
