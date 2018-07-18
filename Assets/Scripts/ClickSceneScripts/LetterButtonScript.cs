using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButtonScript : MonoBehaviour {

	public Text LetterCharacter;
    public bool LowerBox;
    public bool IsDistractor;



	public void OnButtonClick()
	{
        //call weiter geben
        TaskController.Instance.LetterButtonGetsClicked(this);
	}

	public void SetInteractable(bool b)
	{
		this.GetComponent<Button>().interactable = b;
	}
}
