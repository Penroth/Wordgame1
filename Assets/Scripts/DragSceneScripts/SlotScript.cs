using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation

    public void OnDrop(PointerEventData eventData)
    {
        //new holder has no letter to hold
        if (!item)
        {
            LetterTextScript.itemBeingDragged.transform.parent.GetComponent<LetterHolderDragScript>().ReleaseButton();
            LetterTextScript.itemBeingDragged.transform.SetParent(transform);
            item.GetComponentInParent<LetterHolderDragScript>()
                .PlaceButton(LetterTextScript.itemBeingDragged.GetComponent<LetterTextScript>());
        }
        //new holder has already a letter to hold
        else
        {
            Debug.Log("kommt rein 1");
            var placedLetter = this.GetComponent<LetterHolderDragScript>().TakenLetter;
            var holderOfPlacedLetter = placedLetter.Holder;
            var draggingItem = LetterTextScript.itemBeingDragged.GetComponent<LetterTextScript>();
            var holderOfDraggedItem = draggingItem.Holder;

            if (placedLetter.LowerBox == true && draggingItem.LowerBox == true)
            {

                Debug.Log("kommt rein 2");
            }
            else
            {
                Debug.Log("kommt rein 3");
                //release both letters from holder, tmp store letter in start to swap correctly
                holderOfPlacedLetter.ReleaseButton();
                Debug.Log("holder nr " + holderOfPlacedLetter.transform.GetSiblingIndex() + " of " + holderOfPlacedLetter.transform.parent);
                TaskControllerDragScript.Instance.TmpPlaceLetterInStart(placedLetter);
                holderOfDraggedItem.ReleaseButton();
                Debug.Log("holder nr " + holderOfDraggedItem.transform.GetSiblingIndex() + " of " + holderOfDraggedItem.transform.parent);
                //place in each others holder
                holderOfPlacedLetter.PlaceButton(draggingItem);
                placedLetter.Holder.ReleaseButton();
                holderOfDraggedItem.PlaceButton(placedLetter);
            }
        }




        //call task controller to enable chk btn with help of lists
    }

    #endregion
}