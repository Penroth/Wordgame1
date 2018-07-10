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
        //physically place it in upper
        button.PlaceInUpper(this);
    }

    public void SetInteractableRelease(bool b)
    {
        this.GetComponent<Button>().interactable = b;
    }

    public void ReleaseButton()
    {
        if (TakenLetter != null)
        {
            TakenLetter.PlaceInLower();

            //sets upper position to unoccupied occupied
            this.IsTaken = false;
            TakenLetter = null;
        }
    }
}