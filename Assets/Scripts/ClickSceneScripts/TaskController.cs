﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public class TaskController : MonoBehaviourSingleton<TaskController>
{

	public GameObject MenuPopup;
	//lower box
    public GameObject StartBox;
	//button
    public GameObject LetterButtonPrefab;
	//upperbox
    public GameObject TargetBox;
	//empty holder button prefab
    public GameObject ButtonHolderPrefab;
	//check box 
	public GameObject CheckBox;
	//check button
	public GameObject CheckButtonPrefab;
	//list of words to work with
    public List<WordItem> Words;
	//greencheckBox for positioning 
	public GameObject greenMarkBox;
	//green check mark for correct answers
	public GameObject greenCheck;
	//redmarkBox for positioning 
	public GameObject RedMarkBox;
	//red x for wrong answers
	public GameObject redX;
	//release button box
	public GameObject ReleaseButtonBox;
	//release button
	public GameObject ReleaseButtonPrefab;
	//listen button box for testing purposes
	public GameObject ListenBox;
	//listen button for testing
	public GameObject ListenButton;
	//menu button box
	public GameObject MenuButtonBox;
	//menu button
	public GameObject MenuButton;

	private CheckButtonScript _checkButtonScript;
	private ReleaseButtonScript _releaseButtonScript;
	//private FinishClickScript _finishClickScript;
	private MenuButtonClickScript _menuButtonScript;
	private ListenClickScript _listenScript;

	//for displaying audio
	public AudioSource ClipSource;


	//create list for upperletterbox
    public List<LetterHolderScript> _targetHolderList = new List<LetterHolderScript>();

	//create List for lowerletterbox
    public List<LetterHolderScript> _startHolderList = new List<LetterHolderScript>();

	//create List for all letters (distractors + wordletters)
    public List<LetterButtonScript> _letterList = new List<LetterButtonScript>();

	//create List for all letters (distractors + wordletters)
	public List<LetterButtonScript> _trueLetters = new List<LetterButtonScript>();

	//pushes letters from startbox to a list
    private List<LetterButtonScript> _startButtonList
    {
        get
        {
            return (from letter in _letterList
                where letter.LowerBox
                select letter).ToList();
        }
    }

	//list with the letters in the upperbox
    private List<LetterButtonScript> _targetButtonList
    {
        get
        {
            return (from letter in _letterList
                where !letter.LowerBox
                select letter).ToList();
        }
    }

    public LetterHolderScript FreeStartHolderButton
    {
        get
        {
            return (from letter in _startHolderList where !letter.IsTaken select letter).FirstOrDefault();
        }
    }

	public string filepath 
	{ 
		get 
		{ 
			return (TaskControllerStartScene.filepath); 
		} 
	}

	public static Stopwatch SW = new Stopwatch();

	//for choosing a WordItem
	public int wordItemCount = 0;
	//counter for back button
	public int backCounter = 0;

    void Start()
    {
        PrepareScene();
    }
		

    // Use this for initialization
    public void PrepareScene()
    {
		SW.Stop();
		SW.Reset();
        #region Inizialisierung der StartBox



		WordItem useWord = Words[wordItemCount];


		//instantiate CheckButton
		var checkButtonGo = Instantiate(CheckButtonPrefab, CheckBox.transform) as GameObject;
		_checkButtonScript = checkButtonGo.GetComponent<CheckButtonScript>();
		_checkButtonScript.SetInteractable(false);

		//instantiate release Button
		var releaseButtonGo = Instantiate(ReleaseButtonPrefab, ReleaseButtonBox.transform) as GameObject;
        _releaseButtonScript = releaseButtonGo.GetComponent<ReleaseButtonScript>();
        _releaseButtonScript.SetInteractableRelease(false);

		//instantiate listen button
		var listenButtonGo = Instantiate(ListenButton, ListenBox.transform);
		_listenScript = listenButtonGo.GetComponent<ListenClickScript>();
		_listenScript.SetInteractable(true);

		//instantiate menu button
		var menuButtonGo = Instantiate(MenuButton, MenuButtonBox.transform);
		_menuButtonScript = menuButtonGo.GetComponent<MenuButtonClickScript>();
		_menuButtonScript.SetInteractable(true);


        //instantiate distractor buttons and add them to the lowerboxlist
        foreach (var distractorStr in useWord.Distractors)
        {
			//instantiate button holder in startBox
            var holderGo = Instantiate(ButtonHolderPrefab, StartBox.transform);

			//instantiate Button
            var letterGo = Instantiate(LetterButtonPrefab, holderGo.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
			//set textvalue of button to distractor char
            letterScript.LetterCharacter.text = distractorStr;
			//set button to the lower box and to a disctractor
            letterScript.LowerBox = true;
            letterScript.IsDistractor = true;
			//add distractor to letterlist
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());
            //Set button position to occupied in lower box
            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
			//? ##tell the holderGo (and its script) that he is holding "letterScript" now. This is the button gameobject with the letter in it. So he knows that he is holding the letter, by setting the "takenletter" variable (if we need it later on)
            holderGo.GetComponent<LetterHolderScript>().TakenLetter = letterScript;
			//add button to holder lowerbox list 
            _startHolderList.Add(holderGo.GetComponent<LetterHolderScript>());
        }

		//instantiate wordletter buttons and add them to the lowerboxlist
        foreach (var letter in useWord.Word)
        {
			//instantiate lower button box
            var holderGo = Instantiate(ButtonHolderPrefab, StartBox.transform);
			//instantiate button
            var letterGo = Instantiate(LetterButtonPrefab, holderGo.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
			//set textvalue of button to wordletter char
            letterScript.LetterCharacter.text = letter.ToString();
			//set button to lower box and to not a distractor
            letterScript.LowerBox = true;
            letterScript.IsDistractor = false;
			//add letter to letterlist
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());
			//set buttonposition to occupied in lower box
            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
			//? ## same as above, but with the correctButtonLetter
            holderGo.GetComponent<LetterHolderScript>().TakenLetter = letterScript;
			//add button to lowerbox list
            _startHolderList.Add(holderGo.GetComponent<LetterHolderScript>());
        }

        //shuffle the buttons
        foreach (Transform child in StartBox.transform)
        {
            child.SetSiblingIndex(Random.Range(0, StartBox.transform.childCount));
        }

        #endregion

        #region Inizialisierung der TargetBox

        // creates an empty box in the upper box for each character in the word letter
        foreach (var letter in useWord.Word)
        {
			//instantiates the gameobjects
            var letterGo = Instantiate(ButtonHolderPrefab, TargetBox.transform) as GameObject;
            var targetButton = letterGo.GetComponent<LetterHolderScript>();
			//sets the empty box to not yet occupied
            targetButton.IsTaken = false;
			//add an entry in the upperbox list
            _targetHolderList.Add(targetButton);
        }

        #endregion

		Listen ();
		SW.Start();
    }


	//activates on click on button and set button in upperbox and place position of button to next free target
    public void LetterButtonGetsClicked(LetterButtonScript currentLetterButton)
    {
        var startHolderButton = _startHolderList.Find(script => script.TakenLetter == currentLetterButton);

        //if the clicked button is in the lower box 
        if (currentLetterButton.LowerBox)
        {
            //check to see if there are any free button holder; does this list has any item which is not taken?
            if (_targetHolderList.Any(holder => !holder.IsTaken))
            {
                //there are some free ones
                
                //? ##create a local variable (only allowed in this function). in the _targetHolderList (=all button holder gameobjects (=that can store and hold button letters) in upper box) get the first gameobject/script that is not taken and if there is one, set it as the value of the new variable firstFreeTarget
                var firstFreeTargetButton = _targetHolderList.FirstOrDefault(letter => !letter.IsTaken);
                startHolderButton.ReleaseButton();
                firstFreeTargetButton.PlaceButton(currentLetterButton);
            }
            else
            {
                //there are no free ones
                Debug.Log("no space in target for the clicked button ... what to do? ;)");
            }
        }

        //check if upper box is full, if so set check interactable
		if (_targetHolderList.Last<LetterHolderScript> ().IsTaken) {
			_checkButtonScript.SetInteractable (true);
			//sets all letter buttons to not interactable
			_letterList.ForEach(letter => letter.SetInteractable(false));

		} 

		if (_targetHolderList.First<LetterHolderScript> ().IsTaken) 
		{
			_releaseButtonScript.SetInteractableRelease (true);
		}
    }

	public void ReleaseButtonClick ()
	{
		backCounter++;
		_checkButtonScript.SetInteractable (false);
        //get last object of _targetholderlist
        var lastTakenHolder = _targetHolderList.LastOrDefault(holder => holder.IsTaken);
	    var buttonToPlaceInStart = lastTakenHolder.TakenLetter;
        lastTakenHolder.ReleaseButton();
        FreeStartHolderButton.PlaceButton(buttonToPlaceInStart);

	    if (!_targetHolderList.First<LetterHolderScript>().IsTaken)
	    {
	        _releaseButtonScript.SetInteractableRelease(false);
	    }
		//sets all letter buttons to interactable again
		_letterList.ForEach(letter => letter.SetInteractable(true));
    }


	public void Check(CheckButtonScript button)
	{

		//count of all Worditems for scene switch once all words are done
		int wordItemMax = Words.Capacity;

		WordItem currentWordItem = Words[wordItemCount];
		string currentWord = currentWordItem.Word;


		var upperWord = "";

		foreach (var upperLetter in _targetHolderList) 
		{
			var upperButtonChar = upperLetter.TakenLetter.LetterCharacter.text;
			upperWord = upperWord + upperButtonChar;
		}

		writeToText (upperWord);
		if (currentWord.Equals(upperWord) ) {
			//switch scene if list is empty, counter is out of bounds
			// start coroutine in der erst feedback anbgegeben wird, dann feedback gelöscht wird, dann scene cleanup, + scene switch
			//show right symbol
			StartCoroutine(ShowPositiveFeedback());


            
		} else {
            //show wrong symbol
		    StartCoroutine(ShowNegativeFeedback());
		}
	}

    public void CleanupScene()
    {
        //counter increase
		wordItemCount++;
        //children gameobjects of upper and lower box container destroy
        foreach (var holderItem in _targetHolderList)
        {
			Destroy(holderItem.gameObject);
        }
		foreach (var startItem in _startButtonList) 
		{
			Destroy (startItem.gameObject);
		}
		foreach (var letter in _letterList) 
		{
			Destroy (letter.gameObject);
		}
		foreach (var startHolder in _startHolderList) 
		{
			Destroy (startHolder.gameObject);
		}
		Destroy (_checkButtonScript.gameObject);
		Destroy (_releaseButtonScript.gameObject);
		//Destroy (_finishClickScript.gameObject);
		Destroy (_listenScript.gameObject);
		Destroy (_menuButtonScript.gameObject);
		_trueLetters.Clear ();
		_startButtonList.Clear ();
        _targetHolderList.Clear();
		_letterList.Clear ();
		_startHolderList.Clear ();
    }

    public IEnumerator ShowNegativeFeedback()
    {
		//step 0
		//set check and release to not interactable
		_checkButtonScript.SetInteractable(false);
		_releaseButtonScript.SetInteractableRelease (false);
		_menuButtonScript.SetInteractable (false);
		_listenScript.SetInteractable (false);
		_letterList.ForEach(letter => letter.SetInteractable(false));
        //step 1
        //instanziiere bild mit rotem X in der mitte vom screen
		var wrongMark = Instantiate(redX, RedMarkBox.transform) as GameObject;
        yield return new WaitForSeconds(3f);
        //step 2
        //kill bild mit rotem x in der mitte
		Destroy(wrongMark);
		//step 4 
		//set check and release interactable again
		_checkButtonScript.SetInteractable(true);
		_releaseButtonScript.SetInteractableRelease(true);
		_listenScript.SetInteractable (true);
		_letterList.ForEach(letter => letter.SetInteractable(true));

    }
	public IEnumerator ShowPositiveFeedback()
	{
		int wordItemMax = Words.Capacity;
		//step 0 
		//set check and release to not interactable
		_checkButtonScript.SetInteractable(false);
		_releaseButtonScript.SetInteractableRelease (false);
		_listenScript.SetInteractable (false);
		_menuButtonScript.SetInteractable (false);
		//step 1
		//instanziiere bild mit rotem X in der mitte vom screen
		var rightMark = Instantiate(greenCheck, greenMarkBox.transform) as GameObject;
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
			backCounter = 0;
			PrepareScene();
		}


	}

	public void writeToText(string upperWord)
	{
		var rt = SW.ElapsedMilliseconds;

		WordItem currentWordItem = Words[wordItemCount];
		string currentWord = currentWordItem.Word;
		string writeToLine = currentWord + ";" + upperWord + ";" + rt + ";" + backCounter;
		StreamWriter writer = new StreamWriter(filepath, true, Encoding.UTF8);
		writer.WriteLine(writeToLine);
		writer.Close();

	}

	public void Listen()
	{
		WordItem currentWordItem = Words[wordItemCount];
		string currentWordName = currentWordItem.name;
		StartCoroutine(PlayAudio(currentWordName));
	}

	public IEnumerator PlayAudio(string currentWordName)
	{
		//step 1, deactivate all buttons
		_menuButtonScript.SetInteractable (false);
		_listenScript.SetInteractable (false);
		_checkButtonScript.SetInteractable (false);
		_releaseButtonScript.SetInteractableRelease (false);
		_letterList.ForEach(letter => letter.SetInteractable(false));

		//step 2 get the audiofiles
		ClipSource.GetComponent<AudioSource> ();
		var currentWordAudio = Resources.Load<AudioClip>("Audioclips/" + currentWordName);
		var currentSentenceAudio = Resources.Load<AudioClip> ("Audioclips/" + currentWordName + "_satz");

		//get the length of the audiofiles
		var clipWordLength = currentWordAudio.length;
		var clipSentenceLength = currentSentenceAudio.length;

		//play the word file
		ClipSource.clip = currentWordAudio;
		ClipSource.Play ();
		//wait for the file to be finished
		yield return new WaitForSeconds(clipWordLength);
		yield return new WaitForSeconds(1f);
		//play the sentence file
		ClipSource.clip = currentSentenceAudio;
		ClipSource.Play ();
		//wait for the file to be finished
		yield return new WaitForSeconds(clipSentenceLength);
		yield return new WaitForSeconds(1f);
		//play the word again
		ClipSource.clip = currentWordAudio;
		ClipSource.Play ();
		//wait
		yield return new WaitForSeconds(clipWordLength);
		yield return new WaitForSeconds(1f);
		//last step activate all buttons again
		_menuButtonScript.SetInteractable (true);
		_listenScript.SetInteractable (true);
		//_checkButtonScript.SetInteractable (true);
		_releaseButtonScript.SetInteractableRelease (true);
		_letterList.ForEach(letter => letter.SetInteractable(true));
	}

	public void OpenPopup()
	{
		MenuPopup.SetActive(true);
	}
	public void ClosePopup()
	{
		MenuPopup.SetActive(false);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene ("StartScene");
	}
		
}