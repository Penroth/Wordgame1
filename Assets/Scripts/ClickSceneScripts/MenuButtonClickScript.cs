using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClickScript : MonoBehaviour {

	public void OnMenuButtonDrag()
	{
		TaskController.Instance.BackToMenu ();
	}
}
