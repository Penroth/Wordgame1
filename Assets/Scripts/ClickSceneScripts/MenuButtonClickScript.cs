using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClickScript : MonoBehaviour {

	public void OnMenuButtonClick()
	{
		TaskController.Instance.BackToMenu ();
	}
}
