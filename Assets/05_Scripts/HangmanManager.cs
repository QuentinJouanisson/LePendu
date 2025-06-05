using System.Collections.Generic;
using NUnit.Framework;
using Pendu.inputhandler;
using UnityEngine;
using PenduAnimation;


namespace Pendu.Visual
{
    public class HangmanManager : MonoBehaviour
    {
        [Header("PenduParts")]
        [SerializeField] private List<SpriteRenderer> penduPartsRender = new();
        [SerializeField] private AnimatePenduParts animator;

        private int errors;
                
        void Awake()
        {
            penduPartsRender = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
            penduPartsRender.RemoveAt(0); //Spawn Pedestrial
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
            foreach (SpriteRenderer part in penduPartsRender)
            {
                part.enabled = false;
            }
                       
        }
        public void IncrementErrors()
        {
            //penduPartsRender[errors++].enabled = true; // old method
            if (errors < penduPartsRender.Count)
            {
                SpriteRenderer part = penduPartsRender[errors++];
                part.enabled = true;

                animator.AnimatePenduPart(part.gameObject);
            }

           
        }

       
        
        // Update is called once per frame
        void Update()
        {

        }
    }
}
