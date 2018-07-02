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

		//instantiate CheckButton
		var checkButtonGo = Instantiate(CheckButtonPrefab, CheckBox.transform) as GameObject;
		var checkScript = checkButtonGo.GetComponent<CheckButtonScript>();



		//instantiate distractor buttons and add them to the lowerboxlist
        foreach (var distractorStr in useWord.Distractors)
        {
			//instantiate button holder in startBox
            var holderGo = Instantiate(ButtonHolderPrefab, StartBox.transform);

			//instantiate Button
            var letterGo = Instantiate(LetterButtonPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set gameobject parent of letter to button holder
            letterGo.transform.SetParent(holderGo.transform);
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
            var letterGo = Instantiate(LetterButtonPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set parent of letter to holder 
            letterGo.transform.SetParent(holderGo.transform);
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

    }


	//activates on click on button
    public void ButtonGetsClicked(LetterButtonScript button)
    {
        //set button in upperbox and place position of button to next free target ##only do this stuff, if the clicked button was in the lower box in the first place
        if (button.LowerBox)
        {
            // ##you have to check if there is a not taken gameobject at all, otherwise it will return a nullpointer error if there are no free button holders.
            //check to see if there are any free button holder; does this list has any item which is not taken?
            if (_targetHolderList.Any(holder => !holder.IsTaken))
            {
                //there are some free ones
                
                //? ##create a local variable (only allowed in this function). in the _targetHolderList (=all button holder gameobjects (=that can store and hold button letters) in upper box) get the first gameobject/script that is not taken and if there is one, set it as the value of the new variable firstFreeTarget
                var firstFreeTarget = _targetHolderList.FirstOrDefault(letter => !letter.IsTaken);
                //sets the position to occupied
                firstFreeTarget.IsTaken = true;
                //assigns the button to parent 
                button.gameObject.transform.SetParent(firstFreeTarget.transform);
                //transforms button to right position
                button.GetComponent<RectTransform>().position = firstFreeTarget.GetComponent<RectTransform>().position;
                //button is no longer in the lower box
                button.LowerBox = false;
            }
            else
            {
                //there are no free ones
                Debug.Log("no space in target for the clicked button ... what to do? ;)");
            }

        }
    }


	public void Check(CheckButtonScript button)
	{
		
		var upperWord = "";
		var lowerWord = "";
		Debug.Log ("checkanfang");
		foreach (var upperLetter in _targetHolderList) 
		{
			var upperButtonChar = upperLetter.TakenLetter.LetterCharacter.text;
			upperWord = upperWord + upperButtonChar;
		}
		foreach (var lowerLetter in _letterList) 
		{
			if (!lowerLetter.IsDistractor) 
			{
				var lowerButtonChar = lowerLetter.LetterCharacter.text;
				lowerWord = lowerWord + lowerButtonChar;
			}

		}

		if (lowerWord == upperWord) {
			Debug.Log ("heyo, passt");
		} else {
			Debug.Log (upperWord);
		}
	}
}