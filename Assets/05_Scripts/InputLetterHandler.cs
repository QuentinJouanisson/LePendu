using UnityEngine;
using Pendu.lettermemory;
using Pendu.Keyboard;
using System;
using Pendu.Visual;
using Pendu.wordscontroller;
using Pendu.GameSession;
using PenduAnimation;
using NUnit.Framework;
using System.Collections.Generic;


namespace Pendu.inputhandler
{

    public class InputLetterHandler : MonoBehaviour
    {

        [SerializeField] private VirtualKeyboard virtualKeyboard;
        [SerializeField] private LetterMemory letterMemory;
        [SerializeField] public HangmanManager hangmanManager;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameVictoryPanel;
        [SerializeField] private int maxErrors = 10;
        [SerializeField] private WordPicker wordPicker;
        [SerializeField] private SplitWordToLetters splitWordToLetters;
        public int MaxErrors => maxErrors;

        
        public event Action<char> OnCorrectLetter;
        public event Action<char> OnIncorrectLetter;
        public event Action<char> OnAlreadyTriedLetter;
        public event Action OnWordCompleted;
        
        [SerializeField] private UIAnimator uIAnimator;

        private int errorCount = 0;
        
        public void ShowMenu()
        {
            uIAnimator.DrawInGameObjects();
        }
        public void HideMenu()
        {
            uIAnimator.DrawOutGameObjects();
        }

        void Start()
        {
            HideMenu();
            virtualKeyboard.OnLetterPressed += HandleLetter;
            gameOverPanel.SetActive(false);
            gameVictoryPanel.SetActive(false);
        }
        private void TriggerGameOver()
        {
            ShowMenu();
            Debug.Log("GameOverTriggered");
            gameOverPanel.SetActive(true);            
            gameVictoryPanel.SetActive(false);            
            virtualKeyboard.SetIntteractable(false);
            virtualKeyboard.gameObject.SetActive(false);
            wordPicker.ResetUsedWords();
        }
        private void TriggerVictory()
        {
            ShowMenu();
            gameOverPanel.SetActive(false);
            gameVictoryPanel.SetActive(true);
            Debug.Log("VICTORY");
            gameVictoryPanel.SetActive(true);            
            virtualKeyboard.SetIntteractable(false);
            GameSessionManager.Instance.RegisterVictory(wordPicker.CurrentWord, errorCount);
        }

        public void ResetGame()
        {
            HideMenu();            
            errorCount = 0;
            
            if(gameOverPanel != null)
            {
                HideMenu();
                //gameOverPanel.SetActive(false);
            }
            if(gameVictoryPanel != null)
            {
                HideMenu();
                //gameVictoryPanel.SetActive(false);
            }
            if (hangmanManager != null) 
            {
                hangmanManager.ResetPendu();
            }
            if (virtualKeyboard != null)
            {
                virtualKeyboard.OnLetterPressed -= HandleLetter; //desabonne du handle
                virtualKeyboard.OnLetterPressed += HandleLetter; //reabonne
                virtualKeyboard.SetIntteractable(true);
                virtualKeyboard.gameObject.SetActive(true);
            }
            wordPicker.ResetUsedWords();
            wordPicker.PickNewWord();
            string newWord = wordPicker.CurrentWord;
            splitWordToLetters.InitNewWord(newWord);            
            letterMemory.Init(newWord);

            OnCorrectLetter?.Invoke('\0');
        }

        public void NextWord()
        {
            HideMenu();
            string previousWord = wordPicker.CurrentWord;

            if(gameOverPanel != null)
            {
                HideMenu();
                //gameOverPanel.SetActive(false);
            }
            if (gameVictoryPanel != null)
            {
                HideMenu();
                //gameVictoryPanel.SetActive(false);
            }
            //if (hangmanManager != null)
            //{
            //    hangmanManager.ResetPendu();
            //}
            if (virtualKeyboard != null)
            {
                virtualKeyboard.OnLetterPressed -= HandleLetter; //desabonne du handle
                virtualKeyboard.OnLetterPressed += HandleLetter; //reabonne
                virtualKeyboard.SetIntteractable(true);
                virtualKeyboard.gameObject.SetActive(true);
            }
            wordPicker.PickNewWord();
            string newWord = wordPicker.CurrentWord;
            splitWordToLetters.InitNewWord(newWord);
            letterMemory.Init(newWord);

            OnCorrectLetter?.Invoke('\0');


        }
        void HandleLetter(char c)
        {
            var result = letterMemory.TestLetter(c);

            switch (result)
            {
                case LetterMemory.LetterResult.Correct:
                    OnCorrectLetter?.Invoke(c);
                    if (letterMemory.IsComplete())
                    {
                        OnWordCompleted?.Invoke();
                        TriggerVictory();
                    }
                                              
                    break;

                case LetterMemory.LetterResult.Incorrect:
                    errorCount++;
                    OnIncorrectLetter?.Invoke(c);
                    hangmanManager.IncrementErrors();
                    Debug.Log($"Erreur {errorCount} / {maxErrors}");
                    if(errorCount >= maxErrors)
                    {
                        TriggerGameOver();
                        Debug.Log("Defeated");
                        
                        //TriggerGameOver
                    }
                    break;

                case LetterMemory.LetterResult.AlreadyTried:
                    OnAlreadyTriedLetter?.Invoke(c);
                    break;
            }
        }
    }
}
