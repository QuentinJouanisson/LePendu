using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Pendu.GameSession;
using System.Linq;

namespace Pendu.wordscontroller 
{ 
    public class WordPicker : MonoBehaviour
    {
        [SerializeField] private WordListData wordListData ;

        private string currentWord;

        public string CurrentWord => currentWord;

        private HashSet<string> usedWords = new();
              

        public void PickNewWord()
        {
            if (wordListData == null || wordListData.words.Count == 0)
            {
                Debug.LogWarning("List is empty");
                currentWord = string.Empty;
                return;
            }

            var availableWords = wordListData.words
                .Where(w => !GameSessionManager.Instance.HasAlreadyPlayed(w))
                .ToList();

            if (availableWords.Count == 0)
            {
                Debug.Log("tous les mots sont joués, reinit");
                GameSessionManager.Instance.ResetPlayedWords();
                availableWords = new List<string>(wordListData.words);
            }

            currentWord = availableWords[Random.Range(0, availableWords.Count)];
            Debug.Log($"Mot Choisi : {CurrentWord}");

           
        }
              

        public void ResetUsedWords()
        {
            usedWords.Clear();
        }
    }
}
