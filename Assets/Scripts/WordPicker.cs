using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class WordPicker : MonoBehaviour
    
{
    [SerializeField] private TextAsset WordFile;

    //private List<string> Wordlist = new List<string>(); //methode classique a l'ancienne
    private List<string> Wordlist = new();
    void Start()
    {
        LoadWordsFromTextAsset();
        string RandomWord =GetRandomWord();
        Debug.Log("mot chosi = "+ RandomWord);
    }
    void LoadWordsFromTextAsset()
    {
        if (WordFile != null)
        {
            string[] lines =WordFile.text.Split(new[] {'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries); // \r pour les vieux mac
            Wordlist.AddRange(lines);
        }
        else
        {
            Debug.Log("erreur de fichier texte ");
        }
    }

    public string GetRandomWord()
    {
        if(Wordlist.Count == 0)
        {
            Debug.LogWarning("attention liste vide");
            return string.Empty;
        }
        int RandomIndex = Random.Range(0,Wordlist.Count);
        return Wordlist[RandomIndex];
    }
}
