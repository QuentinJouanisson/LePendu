using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using PlasticGui.WorkspaceWindow.QueryViews.Branches;

namespace Pendu.Keyboard
{

    public class VirtualKeyboard : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform keyboardParent;
        [SerializeField] private GameObject letterInputButtonPrefab;                
        [SerializeField] private Color alreadyTriedColor = Color.gray;
        private Color defaultColor;


        private GridLayoutGroup _gridLayoutGroup;
        private Dictionary<char, Button> letterButtons = new Dictionary<char, Button>();

        public void SetIntteractable(bool state)
        {
            foreach(Transform child in keyboardParent)
            {
                Button btn =child.GetComponent<Button>();
                if (btn != null)
                {
                    btn.interactable = state;
                }
            }
        }

        public Action<char> OnLetterPressed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GenerateKeyboard();
        }


        void GenerateKeyboard()
        {            
            for (char C = 'a'; C <= 'z'; C++)
            {
                GameObject buttonGO = Instantiate(letterInputButtonPrefab, keyboardParent);
                buttonGO.name = "key_" + C;

                TextMeshProUGUI tmpText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
                tmpText.text = C.ToString();

                Button btn = buttonGO.GetComponent<Button>();
                char letter = C;

                if (defaultColor == default)
                    defaultColor =btn.colors.normalColor;

                    btn.onClick.AddListener(() =>
                {
                    MarkLetterAsUsed(letter);
                    OnLetterPressed?.Invoke(letter);
                });
            
                letterButtons[letter] = btn;                
            }
        }

        public void MarkLetterAsUsed(char letter)
        {
            if (letterButtons.TryGetValue(letter, out var btn))
            {
                btn.interactable = false;
                SetButtonColor(btn, Color.gray);
            }
        }
        private void SetButtonColor(Button btn, Color color)
        {
            Image image = btn.GetComponentInChildren<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }

        public void ResetKeyboard()
        {
            foreach (var kvp in letterButtons)
            {
                SetButtonColor(kvp.Value, defaultColor);
                kvp.Value.interactable = true;
            }
        }
    }
}