using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Internal;

namespace Pendu.wordscontroller { 
    public class WordPicker : MonoBehaviour
    {
        [SerializeField] private WordListData wordListData ;

        private string currentWord;

        public string CurrentWord => currentWord;

        public void PickNewWord()
        {
            if (wordListData == null || wordListData.words.Count == 0)
            {
                Debug.LogWarning("List is empty");
                currentWord = string.Empty;
                return;
            }

            int randomIndex = Random.Range(0, wordListData.words.Count);
            currentWord = wordListData.words[randomIndex];
            Debug.Log($"Mot Choisi : {currentWord}");
        }        
    }
}
