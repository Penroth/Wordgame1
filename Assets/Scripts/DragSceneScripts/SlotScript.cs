using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

    public static bool Swapped = false;

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
            var placedLetter = this.GetComponent<LetterHolderDragScript>().TakenLetter.gameObject;
            var draggingItem = LetterTextScript.itemBeingDragged;

            if (placedLetter.GetComponent<LetterTextScript>().LowerBox == true &&
                draggingItem.GetComponent<LetterTextScript>().LowerBox == true)
            {
                Debug.Log("beide unten");
            }
            else
            {
                Debug.Log("Swap");
//                //release both letters from holder, tmp store letter in start to Swap correctly
//                holderOfPlacedLetter.ReleaseButton();
//                Debug.Log("holder nr " + holderOfPlacedLetter.transform.GetSiblingIndex() + " of " + holderOfPlacedLetter.transform.parent);
//                TaskControllerDragScript.Instance.TmpPlaceLetterInStart(placedLetter);
//                holderOfDraggedItem.ReleaseButton();
//                Debug.Log("holder nr " + holderOfDraggedItem.transform.GetSiblingIndex() + " of " + holderOfDraggedItem.transform.parent);
//                //place in each others holder
//                holderOfPlacedLetter.PlaceButton(draggingItem);
//                placedLetter.Holder.ReleaseButton();
//                holderOfDraggedItem.PlaceButton(placedLetter);
                Swap(draggingItem, placedLetter);
            }
        }


        //call task controller to enable chk btn with help of lists
        if (TaskControllerDragScript.Instance.targetDragList.Count ==
            TaskControllerDragScript.Instance.targetDragList.Capacity &&
            TaskControllerDragScript.Instance.targetDragList.Count > 0)
        {
            TaskControllerDragScript.Instance.checkButtonScript.SetInteractable(true);
        }
        else
        {
            TaskControllerDragScript.Instance.checkButtonScript.SetInteractable(false);
        }
//		int takenCount = 0;
//		foreach (var letter in TaskControllerDragScript.Instance._targetHolderDragList) 
//		{
//			if (letter.IsTaken) {
//				takenCount++;
//			} 
//			else 
//			{
//				takenCount--;
//			}
//		}
//		if (takenCount == TaskControllerDragScript.Instance.targetDragList.Capacity) {
//			TaskControllerDragScript.Instance.checkButtonScript.SetInteractable (true);
//		} 
//		else 
//		{
//			TaskControllerDragScript.Instance.checkButtonScript.SetInteractable (false);
//		}
//
    }

    #endregion


    public void Swap(GameObject dragged, GameObject occupier)
    {
        Swapped = true;
        Debug.Log("draggingitem " + dragged.GetComponentInChildren<Text>().text);
        Debug.Log("placedletter " + occupier.GetComponentInChildren<Text>().text);
//		var draggedHolder = dragged.GetComponent<LetterTextScript>().Holder;
//		var occupiedHolder = occupier.GetComponent<LetterTextScript>().Holder;

        var draggedLetter = dragged.GetComponentInChildren<LetterTextScript>().GetComponentInChildren<Text>().text;
        dragged.GetComponentInChildren<Text>().text = occupier.GetComponentInChildren<Text>().text;
        occupier.GetComponentInChildren<Text>().text = draggedLetter;

        //occupier.GetComponent<LetterHolderDragScript> ().ReleaseButton ();
//		occupier.GetComponentInParent<RectTransform> ().position = posOfDraggedParent;
//		occupier.GetComponent<LetterTextScript> ().Holder = draggedHolder;
//		occupier.transform.SetParent (transform);
//
//		dragged.GetComponentInParent<RectTransform> ().position = tempPos;
//		dragged.GetComponent<LetterTextScript> ().Holder = occupiedHolder;
//		dragged.transform.SetParent (transform);

//		l2.GetComponent<LetterTextScript> ().GetComponent<RectTransform> ().position = l1.GetComponent<LetterTextScript> ().GetComponent<RectTransform> ().position;
//		l2.GetComponent<LetterTextScript> ().Holder = firstHolder;
//		l2.transform.SetParent (transform);
//
//		l1.GetComponent<LetterTextScript>().GetComponent<RectTransform>().position = tempPos;
//		l1.GetComponent<LetterTextScript> ().Holder = secondHolder;
//		l1.transform.SetParent(transform);
//
    }
}