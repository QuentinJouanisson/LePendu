using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordListData", menuName = "Scriptable Objects/WordListData")]
public class WordListData : ScriptableObject
{
    public List<string> words = new();
}
