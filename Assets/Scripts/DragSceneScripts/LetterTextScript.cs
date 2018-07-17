using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterTextScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public Text LetterCharacter;
    public bool LowerBox;

    public bool IsDistractor;
    public LetterHolderDragScript Holder;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        //used to determine if object is dropped to new slot
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //transform.SetParent(TaskControllerDragScript.Instance.DraggingBox.transform);
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //reset letter to old position because dragged letter is not over a new holder
        if (transform.parent == startParent)
        {
            if (this.LowerBox || SlotScript.Swapped)
            {
                //snap back
                transform.position = startPosition;
            }
            else
            {
                Debug.Log("sollte runter placen");
                var firstFreeStartHolder = (from letter in TaskControllerDragScript.Instance._startHolderDragList
                    where !letter.IsTaken
                    select letter).FirstOrDefault();
                this.Holder.ReleaseButton();
                firstFreeStartHolder.PlaceButton(this);
            }
        }

        SlotScript.Swapped = false;
    }

    #endregion
}