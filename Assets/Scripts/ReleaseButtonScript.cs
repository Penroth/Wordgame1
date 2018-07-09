//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//[RequireComponent(typeof(Button))]
//public class ReleaseButtonScript : MonoBehaviour {
//
//	public void OnReleaseButtonClick()
//	{
//		TaskController.Instance.ReleaseButton(this);
//	}
//
//	public void SetInteractableRelease(bool b)
//	{
//		this.GetComponent<Button>().interactable = b;
//	}
//
//	public void ReleaseButton(LetterButtonScript button)
//	{
//
//		//sets the position to occupied
//		this.IsTaken = false;
//		//set the taken letter
//		this.TakenLetter = button;
//		//assigns the button to parent 
//		button.gameObject.transform.SetParent(this.transform);
//		//transforms button to right position
//		button.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
//		//button is no longer in the lower box
//		button.LowerBox = true;
//	}
//
//}
