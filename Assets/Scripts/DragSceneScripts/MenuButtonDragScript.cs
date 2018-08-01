using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class MenuButtonDragScript : MonoBehaviour {

	public void OnMenuButtonClick()
	{
		TaskControllerDragScript.Instance.OpenPopup();
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}


}
