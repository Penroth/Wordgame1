using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonDragScript : MonoBehaviour {

	public void OnMenuButtonClick()
	{
		TaskControllerDragScript.Instance.BackToMenu ();
	}
}
