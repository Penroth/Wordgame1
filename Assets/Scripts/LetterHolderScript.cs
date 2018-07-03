using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ReleaseButton(LetterButtonScript button)
    {
        // if button gets deleted from holder
    }
}
