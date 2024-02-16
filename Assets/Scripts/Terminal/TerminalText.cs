using System;
using TMPro;
using UnityEngine;

namespace Terminal
{

    public class TerminalText : MonoBehaviour
    {

        private static String Font = "consola SDF";
        private static int FontSize = 16;
        private static Color Color = Color.green;
        private static GameObject Parent;

        public static void InputText (string input)
        {
            GameObject text = new();
            TMP_Text textComponent = text.AddComponent<TextMeshProUGUI>();
            textComponent.text = input;
            textComponent.fontSize = FontSize;
            textComponent.color = Color;

            text.transform.SetParent(Parent.transform);
            text.transform.localScale = Vector3.one;
            text.transform.localPosition = Vector3.zero;

            TMP_FontAsset font = Resources.Load<TMP_FontAsset>(Font);
            textComponent.font = font;
        }

        public void Start()
        {
            Parent = GameObject.Find("Content");
        }
    }
}