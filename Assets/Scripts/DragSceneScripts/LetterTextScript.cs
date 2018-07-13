using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterTextScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{

	//public Text LetterCharacter;
	public bool LowerBox;
	public bool IsDistractor;

	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
		
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		//used to determine if object is dropped to new slot
		startParent = transform.parent;
		//GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		this.LowerBox = false;
		itemBeingDragged = null;
		//GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent) 
		{
			transform.position = startPosition;
		}
	}

	#endregion
}
