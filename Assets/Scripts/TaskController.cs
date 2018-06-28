using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class TaskController : MonoBehaviourSingleton<TaskController>
{
	//lower box
    public GameObject StartBox;
	//button
    public GameObject LetterPrefab;
	//upperbox
    public GameObject TargetBox;
	//empty upperbox fields
    public GameObject TargetHolder;
	//list of words to work with
    public List<WordItem> Words;

	//create list for upperletterbox
    public List<LetterHolderScript> _targetHolderList = new List<LetterHolderScript>();

	//create List for lowerletterbox
    public List<LetterHolderScript> _startHolderList = new List<LetterHolderScript>();

	//create List for all letters (distractors + wordletters)
    public List<LetterButtonScript> _letterList = new List<LetterButtonScript>();

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
    private List<LetterButtonScript> _targteButtonList
    {
        get
        {
            return (from letter in _letterList
                where !letter.LowerBox
                select letter).ToList();
        }
    }

//
//    void shuffle(char[] toShuffle)
//    {
//        for (int l = 0; l < toShuffle.Length; l++)
//        {
//            char tmp = toShuffle[l];
//            int rando = Random.Range(l, toShuffle.Length);
//            toShuffle[l] = toShuffle[rando];
//            toShuffle[rando] = tmp;
//        }
//    }

    void Start()
    {
        PrepareScene();
    }

    // Use this for initialization
    public void PrepareScene()
    {
        #region Inizialisierung der StartBox

        
        Debug.Log("start");

		//chose a random word - replace with fixed order later
        int wordcount = Words.Capacity;
        int randomIndex = Random.Range(0, wordcount);
        WordItem useWord = Words[randomIndex];

        //erst holder gameobj erstellen
        //...

		//instantiate distractor buttons and add them to the lowerboxlist
        foreach (var distractorStr in useWord.Distractors)
        {
			//instantiate lower button box
            var holderGo = Instantiate(TargetHolder, StartBox.transform);

            Debug.Log(distractorStr);
			//instantiate Button
            var letterGo = Instantiate(LetterPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set parent of letter to holder 
            letterGo.transform.SetParent(holderGo.transform);
			//set textvalue of button to distractor char
            letterScript.LetterCharacter.text = distractorStr;
			//button is in the lower box and is a disctractor
            letterScript.LowerBox = true;
            letterScript.IsDistractor = true;
			//add distractor to letterlist
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());
            //Set button position to occupied in lower box
            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
			//?
            holderGo.GetComponent<LetterHolderScript>().TakenLetter = letterScript;
			//add button to lowerbox list
            _startHolderList.Add(holderGo.GetComponent<LetterHolderScript>());
        }

		//instantiate wordletter buttons and add them to the lowerboxlist
        foreach (var letter in useWord.Word)
        {
			//instantiate lower button box
            var holderGo = Instantiate(TargetHolder, StartBox.transform);
			//instantiate button
            var letterGo = Instantiate(LetterPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set parent of letter to holder 
            letterGo.transform.SetParent(holderGo.transform);
			//set textvalue of button to wordletter char
            letterScript.LetterCharacter.text = letter.ToString();
			//button is in lower box and not a distractor
            letterScript.LowerBox = true;
            letterScript.IsDistractor = false;
			//add letter to letterlist
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());
			//set buttonposition to occupied in lower box
            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
			//?
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
            var letterGo = Instantiate(TargetHolder, TargetBox.transform) as GameObject;
            var targetButton = letterGo.GetComponent<LetterHolderScript>();
			//sets the empty box to not yet occupied
            targetButton.IsTaken = false;
			//add an entry in the upperbox list
            _targetHolderList.Add(targetButton);
        }

        #endregion

    }

    public void Check()
    {
        //vergleich target liste (converted to string) mit word
    }
	//activates on click on button
    public void ButtonGetsClicked(LetterButtonScript button)
    {
        //set button in upperbox and place position of button to next free target
        if (button.LowerBox)
        {
			//?
            var firstFreeTarget = _targetHolderList.FirstOrDefault(letter => !letter.IsTaken);
			//sets the position to occupied
            firstFreeTarget.IsTaken = true;
			//assigns the button to parent 
            button.gameObject.transform.SetParent(firstFreeTarget.transform);
			//transforms button to right position
            button.GetComponent<RectTransform>().position = firstFreeTarget.GetComponent<RectTransform>().position;
            //button is no longer in the lower bix
			button.LowerBox = false;
        }
    }
}