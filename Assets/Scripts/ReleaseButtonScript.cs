using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReleaseButtonScript : MonoBehaviour
{

    public void OnReleaseButtonClick()
    {
        TaskController.Instance.ReleaseButtonClick();
    }
    public void SetInteractableRelease(bool b)
    {
        this.GetComponent<Button>().interactable = b;
    }

}
