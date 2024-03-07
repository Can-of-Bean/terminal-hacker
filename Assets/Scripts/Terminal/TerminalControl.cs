using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;
using Files;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Terminal
{
    public class TerminalControl : SingletonBehaviour<TerminalControl>
    {
        private static readonly HashSet<char> s_knownGoodCharacters = new HashSet<char>();
        private static readonly HashSet<char> s_knownBadCharacters = new HashSet<char>();

        [SerializeField]
        private TMP_InputField m_textInputField = null!;

        [SerializeField]
        private TextMeshProUGUI m_textDisplay = null!;

        [SerializeField]
        private TextMeshProUGUI m_inputHeaderDisplay = null!;

        [SerializeField]
        private ScrollRect m_textDisplayScrollContainer = null!;

        [SerializeField]
        private string m_inputHeader = String.Empty;

        [SerializeField]
        private string m_startingDirectory = "/home/user";

        public IFileSystem CurrentFileSystem { get; set; } = LocalFileSystem.Instance;

        /// <summary>
        /// Gets or Sets the input header text. This text is what appears above the input field and every time the user enters a command
        /// </summary>
        public string InputHeader
        {
            get => m_inputHeader;
            set
            {
                m_inputHeader = value;
                m_inputHeaderDisplay.text = value;
            }
        }

        /// <summary>
        /// Gets or Sets the text target of this terminal control.
        /// </summary>
        public ITerminalControlTarget? ControlTarget { get; set; } = new CommandControl();

        /// <summary>
        /// An event fired when the user submits input.
        /// </summary>
        public event EventHandler<TerminalInputEventArgs>? RawInputSubmitted; 

        private void Start()
        {
            m_inputHeaderDisplay.text = $"{m_inputHeader}";
            m_textInputField.Select();

            // Set the starting directory
            CurrentFileSystem.ChangeDirectory(m_startingDirectory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log($"User submitted text: {m_textInputField.text}");
                
                // notify the current control target
                ControlTarget?.HandleUserInput(m_textInputField.text);
                
                // notify of raw input in
                RawInputSubmitted?.Invoke(this, new TerminalInputEventArgs(m_textInputField.text));
                
                // clear currently input text
                m_textInputField.text = String.Empty;
            }
            
            // ensure the input field is selected if it is enabled and should be usable
            if (m_textInputField.gameObject.activeSelf)
            {
                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(m_textInputField.gameObject);
                m_textInputField.ActivateInputField();
            }
        }

        /// <summary>
        /// Enables or Disables the user from inputting any text.
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnabledInputState(bool enable)
        {
            m_inputHeaderDisplay.gameObject.SetActive(enable);
            m_textInputField.gameObject.SetActive(enable);
            
            if (enable)
                m_textInputField.ActivateInputField();
            else
            {
                m_textInputField.DeactivateInputField();
                m_textInputField.text = String.Empty;
            }
        }

        public void WriteToConsole(string text)
        {
            m_textDisplay.text += ConvertStringToReadableCharacters(text);

            StartCoroutine(ScrollToBottom());
        }

        public void WriteLineToConsole(string text)
        {
            WriteToConsole(text + "\n");
        }

        private IEnumerator ScrollToBottom()
        {
            yield return new WaitForEndOfFrame();
            m_textDisplayScrollContainer.verticalNormalizedPosition = 0;
        }

        public void ClearConsoleText()
        {
            m_textDisplay.text = String.Empty;
            StartCoroutine(ScrollToBottom());
        }

        private string ConvertStringToReadableCharacters(string input)
        {
            const char badCharacterPlaceholder = '\u25A1';
            
            // create the builder for the result string
            StringBuilder outputBuilder = new StringBuilder();

            // get a collection of unique characters in the text and loop through them
            foreach (char c in input)
            {
                // font supporting the character is known
                if (s_knownGoodCharacters.Contains(c))
                {
                    outputBuilder.Append(c);
                    continue;
                }

                // font not supporting the character is known
                if (s_knownBadCharacters.Contains(c))
                {
                    outputBuilder.Append(badCharacterPlaceholder);
                    continue;
                }
                
                // check if the font does not support the character
                if (!m_textDisplay.font.HasCharacter(c))
                {
                    // then change the character in the output to a placeholder
                    outputBuilder.Append(badCharacterPlaceholder);
                            
                    // record bad character
                    s_knownBadCharacters.Add(c);
                }
                else
                {
                    // record good character
                    s_knownGoodCharacters.Add(c);
                    
                    // allow good character to pass through
                    outputBuilder.Append(c);
                }
            }

            return outputBuilder.ToString();
        }
    }
}