using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ListenDragScript : MonoBehaviour {

	public void ListenDrag()
	{
		TaskControllerDragScript.Instance.Listen ();
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}
}
