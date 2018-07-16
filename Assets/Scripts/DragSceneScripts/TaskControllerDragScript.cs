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
	// hintergrund für buchstaben


	//Start Box 
	public GameObject LowerBox;
    //Target Box 
    public GameObject UpperBox;
    //dragging Box 
    public GameObject DraggingBox;
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

	public CheckButtonDragScript checkButtonScript;

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
	public List<LetterTextScript> targetDragList
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
		checkButtonScript = checkButtonDrag.GetComponent<CheckButtonDragScript>();
		checkButtonScript.SetInteractable(false);




		foreach (var distractorStr in currentWord.Distractors)
		{
			//instantiate button holder in lowerBox
			var holderDrag = Instantiate(ButtonHolderPrefab, LowerBox.transform);
		    var holderDragScript = holderDrag.GetComponent<LetterHolderDragScript>();
		    holderDragScript.Lower = true;

            //instantiate Button
            var letterDrag = Instantiate(LetterDragPrefab, holderDrag.transform) as GameObject;
			var letterScript = letterDrag.GetComponent<LetterTextScript>();
		    letterScript.Holder = holderDragScript;
			//set textvalue to distractor char
			letterScript.GetComponent<Text>().text= distractorStr;
			//set button to the lower box and to a disctractor
			letterScript.LowerBox = true;
			letterScript.IsDistractor = true;
			//add distractor to letterlist
			_letterList.Add(letterDrag.GetComponent<LetterTextScript>());
            //Set button position to occupied in lower box
		    holderDragScript.IsTaken = true;
            //? ##tell the holderGo (and its script) that he is holding "letterScript" now. This is the button gameobject with the letter in it. So he knows that he is holding the letter, by setting the "takenletter" variable (if we need it later on)
		    holderDragScript.TakenLetter = letterScript;
			//add button to holder lowerbox list 
			_startHolderDragList.Add(holderDragScript);
		}

		//instantiate wordletter buttons and add them to the lowerboxlist
		foreach (var letterChar in currentWord.Word)
		{
			//instantiate lower button box
			var holderDrag = Instantiate(ButtonHolderPrefab, LowerBox.transform);
		    var holderDragScript = holderDrag.GetComponent<LetterHolderDragScript>();
		    holderDragScript.Lower = true;

            //instantiate button
            var letterDrag = Instantiate(LetterDragPrefab, holderDrag.transform) as GameObject;
			var letterScript = letterDrag.GetComponent<LetterTextScript>();
		    letterScript.Holder = holderDragScript;

            //set textvalue of button to wordletter char
            letterScript.GetComponent<Text>().text = letterChar.ToString();
			//set button to lower box and to not a distractor
			letterScript.LowerBox = true;
			letterScript.IsDistractor = false;
			//add letter to letterlist
			_letterList.Add(letterDrag.GetComponent<LetterTextScript>());
            //set buttonposition to occupied in lower box
		    holderDragScript.IsTaken = true;
            //? ## same as above, but with the correctButtonLetter
		    holderDragScript.TakenLetter = letterScript;
			//add button to lowerbox list
			_startHolderDragList.Add(holderDragScript);
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
		    targetDrag.Lower = false;
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
		WordItem currentWordItem = Words[wordItemCount];
		string currentWord = currentWordItem.Word;

		var upperWord = "";
		var lowerWord = "";
		foreach (var upperLetter in _targetHolderDragList) 
		{
			var upperChar = upperLetter.GetComponentInChildren<Text>().text;
			upperWord = upperWord + upperChar;
		}

		if (currentWord.Equals(upperWord) ) {
			//switch scene if list is empty, counter is out of bounds
			// start coroutine wo erst feedback anbgegeben wird, dann feedback gelöscht wird, dann scene cleanup, + scene switch
			//show right symvol

			Debug.Log ("passt");
			StartCoroutine(ShowPositiveDragFeedback());



		} else {
			Debug.Log (upperWord);
			Debug.Log (lowerWord);
			//show wrong symbol
			StartCoroutine(ShowNegativeFeedbackDrag());
		}
	}

	public IEnumerator ShowNegativeFeedbackDrag()
	{
		//step 0
		//set check and release to not interactable
		checkButtonScript.SetInteractable(false);
		//step 1
		//instanziiere bild mit rotem X in der mitte vom screen
		var wrongMark = Instantiate(RedXPrefab, RedXBox.transform) as GameObject;
		yield return new WaitForSeconds(3f);
		//step 2
		//kill bild mit rotem x in der mitte
		Destroy(wrongMark);
		//step 4 
		//set check and release interactable again
		checkButtonScript.SetInteractable(true);

	}

	public IEnumerator ShowPositiveDragFeedback()
	{
		//count of all Worditems for scene switch once all words are done
		int wordItemCap = Words.Capacity;
		//step 0 
		//set check and release to not interactable
		checkButtonScript.SetInteractable(false);
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

		if (wordItemCount == wordItemCap)
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
		Destroy (checkButtonScript.gameObject);
		_startDragList.Clear ();
		_targetHolderDragList.Clear();
		_letterList.Clear ();
		_startHolderDragList.Clear ();
	}

    public void TmpPlaceLetterInStart(LetterTextScript letterToStore)
    {
        var firstFreeStartHolder = (from letter in _startHolderDragList where !letter.IsTaken select letter).FirstOrDefault();
        firstFreeStartHolder.PlaceButton(letterToStore);
    }
}
