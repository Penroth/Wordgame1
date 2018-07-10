using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragButtonScript : MonoBehaviour {

	public void OnDragButtonClick()
	{
		Debug.Log ("onbuttonclick dragscript");
		TaskControllerSartScene.Instance.DragOnClick ();
	}
}
