using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace Pendu.gamecontroller
{


    public class GameControler : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Game is quitting");
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
       
        }
    }
}   

