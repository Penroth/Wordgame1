﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Text;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TaskControllerDragScript : MonoBehaviourSingleton<TaskControllerDragScript>
{



    public GameObject MenuPopup;
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
	//listen box
	public GameObject ListenBox;
	//listen Button
	public GameObject ListenButton;
	//menu button box
	public GameObject MenuButtonBox;
	//menu button
	public GameObject MenuButton;
	//list for upperletters 
	public List<LetterHolderDragScript> targetHolderDragList = new List<LetterHolderDragScript>();
	//list for lower letters
	public List<LetterHolderDragScript> _startHolderDragList = new List<LetterHolderDragScript>();
	//list for all letters (distractors + wordletters)
	public List <LetterTextScript> _letterList = new List <LetterTextScript>();
	//list for true letters for finish function (testing purpose)
	public List <LetterTextScript> trueLetters = new List <LetterTextScript>();

    public static Stopwatch SW = new Stopwatch();

	//for displaying audio
	public AudioSource ClipSource;

	public CheckButtonDragScript checkButtonScript;
	//private FinishDragScript _finishDragScript;
	private MenuButtonDragScript _menuButtonDragScript;
	private ListenDragScript _listenScript;

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

	public string filepath 
	{ 
		get 
		{ 
			return (TaskControllerStartScene.filepath); 
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
        SW.Stop();
        SW.Reset();

		//use current Task of TaskItemList
		WordItem currentWord = Words[wordItemCount];

		//instantiate CheckButton
		var checkButtonDrag = Instantiate(CheckButtonPrefab, CheckBox.transform) as GameObject;
		checkButtonScript = checkButtonDrag.GetComponent<CheckButtonDragScript>();
		checkButtonScript.SetInteractable(false);

		//instantiate menu button
		var menuButtonGo = Instantiate(MenuButton, MenuButtonBox.transform);
		_menuButtonDragScript = menuButtonGo.GetComponent<MenuButtonDragScript>();
		_menuButtonDragScript.SetInteractable (true);

		var listenButtonGo = Instantiate(ListenButton, ListenBox.transform);
		_listenScript = listenButtonGo.GetComponent<ListenDragScript>();
		_listenScript.SetInteractable(true);




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
			letterScript.GetComponentInChildren<Text>().text= distractorStr;
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
            letterScript.GetComponentInChildren<Text>().text = letterChar.ToString();
			//set button to lower box and to not a distractor
			letterScript.LowerBox = true;
			letterScript.IsDistractor = false;
			//add letter to letterlist
			_letterList.Add(letterDrag.GetComponent<LetterTextScript>());
			//push true letters to list for finish function
			trueLetters.Add(letterDrag.GetComponent<LetterTextScript>());
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
			targetHolderDragList.Add(targetDrag);
		}
		Listen ();
        //start Stopwatch
        SW.Start();
	}

	public void Check(CheckButtonDragScript button)
	{
        BlockAllLetterRaycasts(true);

		WordItem currentWordItem = Words[wordItemCount];
		string currentWord = currentWordItem.Word;

		var upperWord = "";
		foreach (var upperLetter in targetHolderDragList) 
		{
			var upperChar = upperLetter.GetComponentInChildren<Text>().text;
			upperWord = upperWord + upperChar;
		}

		if (currentWord.Equals(upperWord) ) {
			// start coroutine bei der erst feedback anbgegeben wird, dann feedback gelöscht wird, dann scene cleanup, + scene switch
			//show right symvol
			StartCoroutine(ShowPositiveDragFeedback());

            

		} else {
			//show wrong symbol
			StartCoroutine(ShowNegativeFeedbackDrag());
	    }

	    writeToText(upperWord);
    }

	public IEnumerator ShowNegativeFeedbackDrag()
	{
		//step 0
		//set check and release to not interactable
		checkButtonScript.SetInteractable(false);
		_menuButtonDragScript.SetInteractable (false);
		_listenScript.SetInteractable (false);
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
		_menuButtonDragScript.SetInteractable (true);
		_listenScript.SetInteractable (true);
		//step 5
		//make buttons dragable again
		BlockAllLetterRaycasts(false);

	}

	public IEnumerator ShowPositiveDragFeedback()
	{
		//count of all Worditems for scene switch once all words are done
		int wordItemCap = Words.Capacity;
		//step 0 
		//set check and release to not interactable
		checkButtonScript.SetInteractable(false);
		_menuButtonDragScript.SetInteractable (false);
		_listenScript.SetInteractable (false);

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
		foreach (var holderItem in targetHolderDragList)
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
		//Destroy (_finishDragScript.gameObject);
		Destroy (_listenScript.gameObject);
		Destroy (_menuButtonDragScript.gameObject);
		_startDragList.Clear ();
		targetHolderDragList.Clear();
		_letterList.Clear ();
		_startHolderDragList.Clear ();
		trueLetters.Clear ();
	}


	public void writeToText(string upperWord)
	{
	    var rt = SW.ElapsedMilliseconds;

        WordItem currentWordItem = Words[wordItemCount];
		string currentWord = currentWordItem.Word;
		string writeToLine = currentWord + ";" + upperWord + ";" + rt;
		StreamWriter writer = new StreamWriter(filepath, true, Encoding.UTF8);
		writer.WriteLine(writeToLine);
		writer.Close();
	}

    public void BlockAllLetterRaycasts(bool b)
    {
        var allLetters = FindObjectsOfType<LetterTextScript>().ToList();
        foreach (var letter in allLetters)
        {
            letter.GetComponent<CanvasGroup>().blocksRaycasts = !b;
        }
    }
	
	public void Listen()
	{
		WordItem currentWordItem = Words[wordItemCount];
		string currentWordName = currentWordItem.name;
		StartCoroutine(PlayAudio (currentWordName));
	}

	public IEnumerator PlayAudio(string currentWordName)
	{
		//step 1, deactivate all buttons
		_menuButtonDragScript.SetInteractable (false);
		_listenScript.SetInteractable (false);
		checkButtonScript.SetInteractable (false);
		BlockAllLetterRaycasts(true);

		ClipSource.GetComponent<AudioSource> ();
		var currentWordAudio = Resources.Load<AudioClip>("Audioclips/" + currentWordName);
		var currentSentenceAudio = Resources.Load<AudioClip> ("Audioclips/" + currentWordName + "_satz");
		var clipWordLength = currentWordAudio.length;
		var clipSentenceLength = currentSentenceAudio.length;
		ClipSource.clip = currentWordAudio;
		ClipSource.Play ();
		yield return new WaitForSeconds(clipWordLength);
		yield return new WaitForSeconds(1f);
		ClipSource.clip = currentSentenceAudio;
		ClipSource.Play ();
		yield return new WaitForSeconds(clipSentenceLength);
		yield return new WaitForSeconds(1f);
		ClipSource.clip = currentWordAudio;
		ClipSource.Play ();
		yield return new WaitForSeconds(clipWordLength);
		yield return new WaitForSeconds(1f);
		//last step activate all buttons again
		_menuButtonDragScript.SetInteractable (true);
		_listenScript.SetInteractable (true);
		//checkButtonScript.SetInteractable (true);
		BlockAllLetterRaycasts(false);
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
