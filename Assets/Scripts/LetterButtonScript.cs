using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButtonScript : MonoBehaviour {

	public Text LetterCharacter;
    public bool LowerBox;
    public bool IsDistractor;



	public void OnButtonClick(){
        //call weiter geben
        TaskController.Instance.ButtonGetsClicked(this);
	}

    public void PlaceInLower()
    {
        var firstButton = TaskController.Instance.FreeStartHolderButton;
        Debug.Log(firstButton.transform.GetSiblingIndex());

        //assigns the button to parent 
        this.gameObject.transform.SetParent(firstButton.transform);
        firstButton.TakenLetter = this;
        firstButton.IsTaken = true;

        //transforms button to right position
        this.GetComponent<RectTransform>().position = firstButton.GetComponent<RectTransform>().position;
        //button is no longer in the lower box
        this.LowerBox = true;
    }

    public void PlaceInUpper(LetterHolderScript holderScript)
    {
        //assigns the button to parent 
        this.gameObject.transform.SetParent(holderScript.transform);
        //transforms button to right position
        this.GetComponent<RectTransform>().position = holderScript.GetComponent<RectTransform>().position;
        //button is no longer in the lower box
        this.LowerBox = false;
    }
}
