using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace Pendu.Keyboard
{

    public class VirtualKeyboard : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform keyboardParent;
        [SerializeField] private GameObject letterInputButtonPrefab;

        private GridLayoutGroup _gridLayoutGroup;

        public Action<char> OnLetterPressed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GenerateKeyboard();
        }


        void GenerateKeyboard()
        {
            for(char C = 'a'; C <= 'z'; C++)
            {
                GameObject buttonGO = Instantiate(letterInputButtonPrefab, keyboardParent);
                buttonGO.name = "key_" + C;

                TextMeshProUGUI tmpText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
                tmpText.text = C.ToString();

                Button btn = buttonGO.GetComponent<Button>();
                char Letter = C;
                btn.onClick.AddListener(() => OnLetterPressed?.Invoke(Letter));
            }
        }
    }
}