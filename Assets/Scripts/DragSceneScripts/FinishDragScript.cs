using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDragScript : MonoBehaviour {

	public void FinishWord()
	{
		TaskControllerDragScript.Instance.Finish ();
	}
}
