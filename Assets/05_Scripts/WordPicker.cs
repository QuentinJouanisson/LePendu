using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Internal;

namespace Pendu.wordscontroller { 
    public class WordPicker : MonoBehaviour
    {
        [SerializeField] private WordListData wordListData ;
        
        public string GetRandomWord()
        {
           if (wordListData == null || wordListData.words.Count == 0)
            {
                Debug.LogWarning("word list empty");
                return string.Empty;
            }
           int RandomIndex = Random.Range(0, wordListData.words.Count);
            string chosenWord = wordListData.words[RandomIndex];

            Debug.Log(chosenWord);
            return chosenWord;
        }
        private void Start()
        {
            
        }
    }
}
