using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public sealed class WordItem : ScriptableObject {
	public string Word;
	public List<string> Distractors;

}
