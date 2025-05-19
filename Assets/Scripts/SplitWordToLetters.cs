using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Pendu.Keyboard;
using Pendu.wordscontroller;
using Pendu.lettermemory;


public class SplitWordToLetters : MonoBehaviour
{
    [SerializeField] private WordPicker wordPicker;
    [SerializeField] private RectTransform letterParent;
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private LetterMemory letterMemory;

    public VirtualKeyboard virtualKeyboard;

    private List<TextMeshProUGUI> displayedLetters = new();
    private string chosenWord;

    //
    private Dictionary<char, List<int>> letterOccurences = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        virtualKeyboard.OnLetterPressed += HandleLetterInput;
        string chosenWord = wordPicker.GetRandomWord();
        letterMemory.Init(chosenWord);
        SplitWord(chosenWord);
        InstanciateLetters(chosenWord);
    }

    void HandleLetterInput(char c)
    {
        LetterMemory.LetterResult result = letterMemory.TestLetter(c);
        
        switch (result)
        {
            case LetterMemory.LetterResult.AlreadyTried:
                Debug.Log($"Lettre deja testee : {c} ");
                break;
            case LetterMemory.LetterResult.Correct:
                RevealLetter(c);
                Debug.Log($"Lettres correctes {c}");
                break;
            case LetterMemory.LetterResult.Incorrect:
                Debug.Log($"Mauvaises Lettres {c}");
                break;

        }

        if (letterMemory.IsComplete())
        {
            Debug.Log("Mot Complet trouvé");
        }
        

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
        displayedLetters.Clear();

        for(int i = 0;i < word.Length; i++)
        {
            GameObject LetterGO = Instantiate(letterPrefab, letterParent);
            TextMeshProUGUI tmp = LetterGO.GetComponent<TextMeshProUGUI>();
            tmp.text = "_";
            displayedLetters.Add(tmp);
            
        }
    }
    private void RevealLetter(char c)
    {
        c = char.ToUpper(c);
        if (!letterOccurences.ContainsKey(c)) return;

        foreach (int index in letterOccurences[c])
        {
            displayedLetters[index].text = c.ToString();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
