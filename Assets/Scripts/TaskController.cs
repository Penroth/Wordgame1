using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class TaskController : MonoBehaviourSingleton<TaskController> {

	public GameObject Box;
	public GameObject LetterPrefab;
	//public GameObject LetterName;
	public List<WordItem> Words;


	void shuffle (char[] toShuffle){
		for (int l = 0; l < toShuffle.Length; l++) {
			char tmp = toShuffle [l];
			int rando = Random.Range (l, toShuffle.Length);
			toShuffle [l] = toShuffle [rando];
			toShuffle [rando] = tmp;
		}

	}
	// Use this for initialization
	void Start () {
		Debug.Log ("start"); 
		int wordcount = Words.Capacity;
		int randomIndex = Random.Range (0, wordcount);
		WordItem useWord = Words[randomIndex];

		string displayLetters = useWord.Word;
		for (int j = 0; j < useWord.Distractors.Count; j++) {
			displayLetters = displayLetters.Insert(0, useWord.Distractors [j]);
		}
					
		char[] letters = displayLetters.ToCharArray();
		shuffle (letters);
		foreach (var h in letters) {
				Debug.Log (h);
				var letter = Instantiate (LetterPrefab, Box.transform) as GameObject;
				letter.GetComponentInChildren<Text> ().text = h.ToString ();
		}
	}
}