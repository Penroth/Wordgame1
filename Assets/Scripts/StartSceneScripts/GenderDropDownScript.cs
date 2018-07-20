using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class GenderDropDownScript : MonoBehaviour {

	public void GenderChanged()
	{
		TaskControllerStartScene.Instance.GenderSwitch ();
	}
}
