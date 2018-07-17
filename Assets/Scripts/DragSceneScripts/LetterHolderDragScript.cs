using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterHolderDragScript : MonoBehaviour
{
	public bool IsTaken;
	public LetterTextScript TakenLetter;
    public bool Lower;
    
    public void PlaceButton(LetterTextScript letter)
    {
        //sets the position to occupied
        this.IsTaken = true;
        //set the taken letter
        this.TakenLetter = letter;

        //place letter physically
        letter.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        letter.transform.SetParent(this.transform);
        //adjust attributes
        if (letter.LowerBox != Lower)
        {
            letter.LowerBox = !letter.LowerBox;
        }
        letter.Holder = this;
    }



    public void ReleaseButton()
    {
        if (TakenLetter != null)
        {
            //sets upper position to unoccupied occupied
            this.IsTaken = false;
            TakenLetter = null;
        }
    }
}
