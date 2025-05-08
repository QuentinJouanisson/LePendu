using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Pendu.Keyboard;


public class SplitWordToLetters : MonoBehaviour
{
    [SerializeField] private WordPicker wordPicker;
    [SerializeField] private RectTransform letterParent;
    [SerializeField] private GameObject letterPrefab;
    public VirtualKeyboard virtualKeyboard;


    private Dictionary<char, List<int>> letterOccurences = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        virtualKeyboard.OnLetterPressed += HandleLetterInput;
        string chosenWord = wordPicker.GetRandomWord();
        SplitWord(chosenWord);
        InstanciateLetters(chosenWord);
    }

    void HandleLetterInput(char c)
    {
        Debug.Log("Lettre cliquée : " +  c);
    }

    public void SplitWord(string word)
    {
        letterOccurences.Clear();
        word = word.ToUpper();

        for (int i = 0; i < word.Length; i++)
        {
            char currentChar = word[i];

            if (!letterOccurences.ContainsKey(currentChar))
                letterOccurences[currentChar] = new List<int>();
            letterOccurences [currentChar].Add(i);
        }
        foreach (var pair in letterOccurences)
        {
            Debug.Log($"Lettre ' {pair.Key}' aux positions : {string.Join(",", pair.Value)}");
        }
    }
    private void InstanciateLetters(string word)
    {
        foreach(Transform child in letterParent) Destroy(child.gameObject);

        for(int i = 0;i < word.Length; i++)
        {
            GameObject LetterGO = Instantiate(letterPrefab, letterParent);
            TextMeshProUGUI tmp = LetterGO.GetComponent<TextMeshProUGUI>();
            tmp.text = "_";
            
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
