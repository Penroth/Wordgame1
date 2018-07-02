using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButtonScript : MonoBehaviour {

	public Text LetterCharacter;
    public bool LowerBox;
    public bool IsDistractor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnButtonClick(){
        //call weiter geben
        TaskController.Instance.ButtonGetsClicked(this);
	}
}
