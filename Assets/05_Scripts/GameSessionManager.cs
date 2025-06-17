using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Pendu.GameSession
{
    public class GameSessionManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        public static GameSessionManager Instance {get; private set; }

        public int TotalErrors { get; private set; }
        public int TotalScore { get; private set; }
        public int CurrentMultiplier { get; private set; } = 1;
        
        public HashSet<string> WordsPlayed { get; private set; } = new HashSet<string>();
       
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
       
        public void RegisterVictory(string word, int errorsThisWord)
        {
            int uniqueCount = CountUniqueLetters(word);
            int gained = uniqueCount * CurrentMultiplier;

            TotalScore += gained;
            TotalErrors += errorsThisWord;
            WordsPlayed.Add(word);

            CurrentMultiplier++;
            
        }   
        public void RegisterGameOver()
        {
            CurrentMultiplier = 1;
        }
        private int CountUniqueLetters(string word)
        {
            HashSet<char> set = new();
            foreach (char c in word.ToUpperInvariant()) set.Add(c);
            return set.Count;
        }
        
        public void RegisterWord(string word)
        {
            WordsPlayed.Add(word);
        }

        public bool HasAlreadyPlayed(string word)
        {
            return WordsPlayed.Contains(word);
        }

        public void ResetSession()
        {

            TotalErrors = 0;
            TotalScore = 0;
            WordsPlayed.Clear();
        }
        public void ResetPlayedWords()
        {
            WordsPlayed.Clear();
        }

        // Update is called once per frame
        void Update()
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score : {GameSessionManager.Instance.TotalScore}";
            }
        }
    }
}
