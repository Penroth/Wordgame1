using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class TaskController : MonoBehaviourSingleton<TaskController>
{
    public GameObject StartBox;
    public GameObject LetterPrefab;
    public GameObject TargetBox;
    public GameObject TargetHolder;
    public List<WordItem> Words;

    public List<LetterHolderScript> _targetHolderList = new List<LetterHolderScript>();
    public List<LetterHolderScript> _startHolderList = new List<LetterHolderScript>();

    public List<LetterButtonScript> _letterList = new List<LetterButtonScript>();

    private List<LetterButtonScript> _startButtonList
    {
        get
        {
            return (from letter in _letterList
                where letter.LowerBox
                select letter).ToList();
        }
    }

    private List<LetterButtonScript> _targteButtonList
    {
        get
        {
            return (from letter in _letterList
                where !letter.LowerBox
                select letter).ToList();
        }
    }


    void shuffle(char[] toShuffle)
    {
        for (int l = 0; l < toShuffle.Length; l++)
        {
            char tmp = toShuffle[l];
            int rando = Random.Range(l, toShuffle.Length);
            toShuffle[l] = toShuffle[rando];
            toShuffle[rando] = tmp;
        }
    }

    void Start()
    {
        PrepareScene();
    }

    // Use this for initialization
    public void PrepareScene()
    {
        #region Inizialisierung der StartBox

        
        Debug.Log("start");
        int wordcount = Words.Capacity;
        int randomIndex = Random.Range(0, wordcount);
        WordItem useWord = Words[randomIndex];

        //erst holder gameobj erstellen
        //...

        foreach (var distractorStr in useWord.Distractors)
        {
            var holderGo = Instantiate(TargetHolder, StartBox.transform);

            Debug.Log(distractorStr);
            var letterGo = Instantiate(LetterPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set parent of letter to holder 
            letterGo.transform.SetParent(holderGo.transform);
            letterScript.LetterCharacter.text = distractorStr;
            letterScript.LowerBox = true;
            letterScript.IsDistractor = true;
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());
            
            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
            holderGo.GetComponent<LetterHolderScript>().TakenLetter = letterScript;
            _startHolderList.Add(holderGo.GetComponent<LetterHolderScript>());
        }


        foreach (var letter in useWord.Word)
        {
            var holderGo = Instantiate(TargetHolder, StartBox.transform);

            var letterGo = Instantiate(LetterPrefab, StartBox.transform) as GameObject;
            var letterScript = letterGo.GetComponent<LetterButtonScript>();
            //set parent of letter to holder 
            letterGo.transform.SetParent(holderGo.transform);

            letterScript.LetterCharacter.text = letter.ToString();
            letterScript.LowerBox = true;
            letterScript.IsDistractor = false;
            _letterList.Add(letterGo.GetComponent<LetterButtonScript>());

            holderGo.GetComponent<LetterHolderScript>().IsTaken = true;
            holderGo.GetComponent<LetterHolderScript>().TakenLetter = letterScript;
            _startHolderList.Add(holderGo.GetComponent<LetterHolderScript>());
        }

        //shuffle
        foreach (Transform child in StartBox.transform)
        {
            child.SetSiblingIndex(Random.Range(0, StartBox.transform.childCount));
        }

        #endregion

        #region Inizialisierung der TargetBox

        // useWord.word 
        foreach (var letter in useWord.Word)
        {
            var letterGo = Instantiate(TargetHolder, TargetBox.transform) as GameObject;
            var targetButton = letterGo.GetComponent<LetterHolderScript>();
            targetButton.IsTaken = false;
            _targetHolderList.Add(targetButton);
        }

        #endregion

    }

    public void Check()
    {
        //vergleich target liste (converted to string) mit word
    }

    public void ButtonGetsClicked(LetterButtonScript button)
    {
        //set button in target and place position of button to next free target
        if (button.LowerBox)
        {
            var firstFreeTarget = _targetHolderList.FirstOrDefault(letter => !letter.IsTaken);
            firstFreeTarget.IsTaken = true;
            button.gameObject.transform.SetParent(firstFreeTarget.transform);
            button.GetComponent<RectTransform>().position = firstFreeTarget.GetComponent<RectTransform>().position;
            button.LowerBox = false;
        }
    }
}