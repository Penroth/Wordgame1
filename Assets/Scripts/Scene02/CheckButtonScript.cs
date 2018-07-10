using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CheckButtonScript : MonoBehaviour
{
    public void OnCheckButtonClick()
    {
        //call weiter geben
        TaskController.Instance.Check(this);
    }

    public void SetInteractable(bool b)
    {
        this.GetComponent<Button>().interactable = b;
    }
}