using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour {

	public void onMenuButtonClick()
	{
		TaskControllerEndScene.Instance.MenuButtonClick ();
	}

}
