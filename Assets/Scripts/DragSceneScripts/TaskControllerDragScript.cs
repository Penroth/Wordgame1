using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;

public class TaskControllerDragScript : MonoBehaviourSingleton<TaskControllerDragScript>//, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	//todo: 
	// buchstaben wieder nach unten verschiebbar machen
	// isTaken bei verschieben oben zurück setzen
	// check button interactable wenn liste oben voll, wo?
	// poitives und negatives prefab tauschen
	// hintergrund für buchstaben


	//Start Box 
	public GameObject LowerBox;
	//Target Box 
	public GameObject UpperBox;
	//letter pregab
	public GameObject LetterDragPrefab;
	//empty Button Holder Prefab for lower and upper Box
	public GameObject ButtonHolderPrefab;
	//Check Box
	public GameObject CheckBox;
	//Check Button
	public GameObject CheckButtonPrefab;
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
	public List<LetterHolderDragScript> _targetHolderDragList = new List<LetterHolderDragScript>();
	//list for lower letters
	public List<LetterHolderDragScript> _startHolderDragList = new List<LetterHolderDragScript>();
	//list for all letters (distractors + wordletters)
	public List <LetterTextScript> _letterList = new List <LetterTextScript>();

	private CheckButtonDragScript _checkButtonScript;

	//pushes letters from startbox to a list
	private List<LetterTextScript> _startDragList
	{
		get
		{
			return (from letter in _letterList
				where letter.LowerBox
				select letter).ToList();
		}
	}

	//list with the letters in the upperbox
	private List<LetterTextScript> _targetDragList
	{
		get
		{
			return (from letter in _letterList
				where !letter.LowerBox
				select letter).ToList();
		}
	}



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
		WordItem currentWord = Words[wordItemCount];

		//instantiate CheckButton
		var checkButtonDrag = Instantiate(CheckButtonPrefab, CheckBox.transform) as GameObject;
		_checkButtonScript = checkButtonDrag.GetComponent<CheckButtonDragScript>();
		//_checkButtonScript.SetInteractable(false);




		foreach (var distractorStr in currentWord.Distractors)
		{
			//instantiate button holder in lowerBox
			var holderDrag = Instantiate(ButtonHolderPrefab, LowerBox.transform);

			//instantiate Button
			var letterDrag = Instantiate(LetterDragPrefab, holderDrag.transform) as GameObject;
			var letterScript = letterDrag.GetComponent<LetterTextScript>();
			Debug.Log ("bis hier hin ist alles gut");
			//set textvalue to distractor char
			letterScript.GetComponent<Text>().text= distractorStr;
			//set button to the lower box and to a disctractor
			letterScript.LowerBox = true;
			letterScript.IsDistractor = true;
			Debug.Log ("distractor test");
			//add distractor to letterlist
			_letterList.Add(letterDrag.GetComponent<LetterTextScript>());
			//Set button position to occupied in lower box
			holderDrag.GetComponent<LetterHolderDragScript>().IsTaken = true;
			//? ##tell the holderGo (and its script) that he is holding "letterScript" now. This is the button gameobject with the letter in it. So he knows that he is holding the letter, by setting the "takenletter" variable (if we need it later on)
			holderDrag.GetComponent<LetterHolderDragScript>().TakenLetter = letterScript;
			//add button to holder lowerbox list 
			_startHolderDragList.Add(holderDrag.GetComponent<LetterHolderDragScript>());
		}

		//instantiate wordletter buttons and add them to the lowerboxlist
		foreach (var letterChar in currentWord.Word)
		{
			//instantiate lower button box
			var holderDrag = Instantiate(ButtonHolderPrefab, LowerBox.transform);
			//instantiate button
			var letterDrag = Instantiate(LetterDragPrefab, holderDrag.transform) as GameObject;
			var letterScript = letterDrag.GetComponent<LetterTextScript>();
			//set textvalue of button to wordletter char
			letterScript.GetComponent<Text>().text = letterChar.ToString();
			//set button to lower box and to not a distractor
			letterScript.LowerBox = true;
			letterScript.IsDistractor = false;
			//add letter to letterlist
			_letterList.Add(letterDrag.GetComponent<LetterTextScript>());
			//set buttonposition to occupied in lower box
			holderDrag.GetComponent<LetterHolderDragScript>().IsTaken = true;
			//? ## same as above, but with the correctButtonLetter
			holderDrag.GetComponent<LetterHolderDragScript>().TakenLetter = letterScript;
			//add button to lowerbox list
			_startHolderDragList.Add(holderDrag.GetComponent<LetterHolderDragScript>());
		}

		//shuffle the buttons
		foreach (Transform child in LowerBox.transform)
		{
			child.SetSiblingIndex(Random.Range(0, LowerBox.transform.childCount));
		}

		// creates an empty box in the upper box for each character in the word letter
		foreach (var letter in currentWord.Word)
		{
			//instantiates the gameobjects
			var letterDrag = Instantiate(ButtonHolderPrefab, UpperBox.transform) as GameObject;
			var targetDrag = letterDrag.GetComponent<LetterHolderDragScript>();
			//sets the empty box to not yet occupied
			targetDrag.IsTaken = false;
			//add an entry in the upperbox list
			_targetHolderDragList.Add(targetDrag);
		}

//		if (UpperBox.GetComponentInChildren<LetterHolderDragScript> ().IsTaken = true) 
//		{
//			_checkButtonScript.SetInteractable (true);
//		}
	}

	public void Check(CheckButtonDragScript button)
	{

		//count of all Worditems for scene switch once all words are done
		int wordItemCap = Words.Capacity;

		var upperWord = "";
		var lowerWord = "";
		foreach (var upperLetter in _targetDragList) 
		{
			var upperChar = upperLetter.GetComponent<Text>().text;
			upperWord = upperWord + upperChar;
		}
		foreach (var lowerLetter in _letterList) 
		{
			if (!lowerLetter.IsDistractor) 
			{
				var lowerChar = lowerLetter.GetComponent<Text>().text;
				lowerWord = lowerWord + lowerChar;
			}

		}

		if (lowerWord.Equals(upperWord) ) {
			//switch scene if list is empty, counter is out of bounds
			// start coroutine wo erst feedback anbgegeben wird, dann feedback gelöscht wird, dann scene cleanup, + scene switch
			//show right symvol

			Debug.Log ("passt");
			StartCoroutine(ShowPositiveDragFeedback());



		} else {
			Debug.Log (upperWord);
			//show wrong symbol
			StartCoroutine(ShowNegativeFeedbackDrag());
		}
	}

	public IEnumerator ShowNegativeFeedbackDrag()
	{
		//step 0
		//set check and release to not interactable
		_checkButtonScript.SetInteractable(false);
		//step 1
		//instanziiere bild mit rotem X in der mitte vom screen
		var wrongMark = Instantiate(RedXPrefab, RedXBox.transform) as GameObject;
		yield return new WaitForSeconds(3f);
		//step 2
		//kill bild mit rotem x in der mitte
		Destroy(wrongMark);
		//step 4 
		//set check and release interactable again
		_checkButtonScript.SetInteractable(true);

	}

	public IEnumerator ShowPositiveDragFeedback()
	{
		int wordItemMax = Words.Capacity;
		//step 0 
		//set check and release to not interactable
		_checkButtonScript.SetInteractable(false);
		//step 1
		//instanziiere bild mit rotem X in der mitte vom screen
		var rightMark = Instantiate(GreenMarkPrefab, GreenMarkBox.transform) as GameObject;
		yield return new WaitForSeconds(3f);
		//step 2
		//kill bild mit rotem x in der mitte
		Destroy(rightMark);
		//step 3
		//cleanup scene
		CleanupScene();

		if (wordItemCount == wordItemMax)
		{
			//switch to endscreen
			Debug.Log("feddich, szenenwechsel");
			SceneManager.LoadScene ("EndScene");
		}

		else
		{
			PrepareDragScene();
		}


	}



	public void CleanupScene()
	{
		//counter increase
		wordItemCount++;
		//children gameobjects of upper and lower box container destroy
		foreach (var holderItem in _targetHolderDragList)
		{
			Destroy(holderItem.gameObject);
		}
		foreach (var startItem in _startDragList) 
		{
			Destroy (startItem.gameObject);
		}
		foreach (var letter in _letterList) 
		{
			Destroy (letter.gameObject);
		}
		foreach (var startHolder in _startHolderDragList) 
		{
			Destroy (startHolder.gameObject);
		}
		Destroy (_checkButtonScript.gameObject);
		_startDragList.Clear ();
		_targetHolderDragList.Clear();
		_letterList.Clear ();
		_startHolderDragList.Clear ();
	}
		
}
