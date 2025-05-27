using System.Collections.Generic;
using NUnit.Framework;
using Pendu.inputhandler;
using UnityEngine;


namespace Pendu.Visual
{
    public class HangmanManager : MonoBehaviour
    {
        [Header("PenduParts")]
        [SerializeField] private List<SpriteRenderer> penduParts = new();

        private int errors;
                
        void Awake()
        {
            penduParts = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
            penduParts.RemoveAt(0); //affiche le piedestal
            ResetPendu();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ResetPendu();
        }
        public void ResetPendu()
        {
            errors = 0;
            foreach (SpriteRenderer part in penduParts)
            {
                part.enabled = false;
            }
                       
        }
        public void IncrementErrors()
        {
            penduParts[errors++].enabled = true;           
        }

       
        
        // Update is called once per frame
        void Update()
        {

        }
    }
}
