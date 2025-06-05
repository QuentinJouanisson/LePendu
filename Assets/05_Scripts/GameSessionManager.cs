using UnityEngine;
using System.Collections.Generic;

namespace Pendu.GameSession
{


    public class GameSessionManager : MonoBehaviour
    {
        public static GameSessionManager Instance {get; private set; }

        public int TotalErrors { get; private set; }
        public int Score { get; private set; }
        
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

        public void RegisterVictory(string word, int errors)
        {
            Score++;
            TotalErrors += errors;
            WordsPlayed.Add(word);
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
            Score = 0;
            WordsPlayed.Clear();
        }
        public void ResetPlayedWords()
        {
            WordsPlayed.Clear();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
