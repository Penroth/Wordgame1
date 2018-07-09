using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterHolderScript : MonoBehaviour
{
    public bool IsTaken;
    public LetterButtonScript TakenLetter;

    public void PlaceButton(LetterButtonScript button)
    {

        //sets the position to occupied
        this.IsTaken = true;
        //set the taken letter
        this.TakenLetter = button;
        //assigns the button to parent 
        button.gameObject.transform.SetParent(this.transform);
        //transforms button to right position
        button.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        //button is no longer in the lower box
        button.LowerBox = false;
    }

	public void OnReleaseButtonClick()
	{
		TaskController.Instance.ReleaseButton(this);
	}

	public void SetInteractableRelease(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}

	public void ReleaseButton(LetterHolderScript button)
	{

		//sets upper position to unoccupied occupied
		this.IsTaken = false;

	}
		
}