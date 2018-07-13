using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CheckButtonDragScript : MonoBehaviour {

	public void OnCheckButton()
	{
		//call weiter geben
		TaskControllerDragScript.Instance.Check(this);
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}
}
