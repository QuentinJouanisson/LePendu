using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Pendu.lettermemory
{
    [CreateAssetMenu(fileName = "LetterMemory", menuName = "Scriptable Objects/LetterMemory")]

    public class LetterMemory : ScriptableObject
    {
        public string SecretWord;
        public List<char> TestedLetters = new();

        public void Init(string word)
        {
            SecretWord = word.ToUpper();
            TestedLetters.Clear();
        }

        public enum LetterResult { AlreadyTried, Correct, Incorrect}

        public LetterResult TestLetter(char letter)
        {
            letter = char.ToUpper(letter);

            if(TestedLetters.Contains(letter))
                return LetterResult.AlreadyTried;

            TestedLetters.Add(letter);

            if(SecretWord.Contains(letter))
                return LetterResult.Correct;
            else return LetterResult.Incorrect;

        }

        public bool IsComplete()
        {
            foreach(char c in SecretWord)
            {
                if (!TestedLetters.Contains(c)) return false;
            }
            return true;    
        } 
    }
}
