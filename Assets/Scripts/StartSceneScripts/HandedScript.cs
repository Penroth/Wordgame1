using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class HandedScript : MonoBehaviour {

	public void HandedChanged()
	{
		//string hand = this.gameObject.GetComponent<Dropdown> ().captionText.text.ToString();
		//Debug.Log (hand + "handedscript");
		TaskControllerSartScene.Instance.HandedSwitch();
	}

}
