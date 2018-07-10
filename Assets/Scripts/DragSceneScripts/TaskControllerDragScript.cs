using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerDragScript : MonoBehaviour {

	//Start Box 
	public GameObject LowerBox;
	//Target Box 
	public GameObject UpperBox;
	//empty Button Holder Prefab for lower and upper Box
	public GameObject ButtonHolderPrefab;
	//Check Box
	public GameObject CheckBox;
	//Check Button
	public GameObject CheckButtonPrefab;
	//Release Box
	public GameObject ReleaseBox;
	//Release Button
	public GameObject ReleaseButtonPrefab;
	//List of Words to Work with
	public List<WordItem> Words;
	//green Mark Box
	public GameObject GreenMarkBox;
	//green Mark for right answers
	public GameObject GreenMarkPrefab;
	//wrong Mark Box
	public GameObject RedXBox;
	//red X for wrong answers
	public GameObject RedXPrefab;

	//list for upperletters 

	//list for lower letters

	//list for all letters (distractors + wordletters)

	//for choosing a WordItem
	public int wordItemCount = 0;

	// Use this for initialization
	void Start () 
	{
		PrepareDragScene ();
	}

	public void PrepareDragScene()
	{
		//use current Task of TaskItemList
		WordItem useWord = Words[wordItemCount];


		//instantiate CheckButton
		var checkButtonDrag = Instantiate(CheckButtonPrefab, CheckBox.transform) as GameObject;
//		_checkButtonScript = checkButtonGo.GetComponent<CheckButtonScript>();
//		_checkButtonScript.SetInteractable(false);

		//instantiate release Button
		var releaseButtonDrag = Instantiate(ReleaseButtonPrefab, ReleaseBox.transform) as GameObject;
//		_releaseButtonScript = releaseButtonGo.GetComponent<ReleaseButtonScript>();
//		_releaseButtonScript.SetInteractableRelease(false);


	}

}
