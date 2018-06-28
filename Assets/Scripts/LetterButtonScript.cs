using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButtonScript : MonoBehaviour {

	public string Letter;
	public Text LetterCharacter; 
	public bool lowerBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnButtonCLick(){
		Debug.Log ("hihuhu");
		if (lowerBox)
			lowerBox = false;
		//pos ändern
	}

	public void setLetter(char l){
		//LetterCharacter.GetComponentInChildren<Text> ().text = l.ToString();
		//LetterCharacter.transform.gameObject.GetComponentInParent<Text>().text= l.ToString();
		//LetterCharacter.text = l.ToString();
	}
}
