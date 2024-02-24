using System;
using System.Collections.Generic;
using Files;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Terminal
{
    public class TerminalControl : SingletonBehaviour<TerminalControl>
    {
        [SerializeField]
        private TMP_InputField m_textInputField = null!;

        [SerializeField]
        private TextMeshProUGUI m_textDisplay = null!;

        [SerializeField]
        private TextMeshProUGUI m_inputHeaderDisplay = null!;

        [SerializeField]
        private string m_inputHeader = String.Empty;

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
        public TerminalControlTarget? ControlTarget { get; set; } = new LoopbackControlTarget();

        /// <summary>
        /// An event fired when the user submits input.
        /// </summary>
        public event EventHandler<TerminalInputEventArgs>? RawInputSubmitted; 

        private void Start()
        {
            m_inputHeaderDisplay.text = $"{m_inputHeader} {CurrentFileSystem.CurrentDirectory.GetPath()}>";
            m_textInputField.Select();
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
            m_textDisplay.text += text;
        }

        public void WriteLineToConsole(string text)
        {
            WriteToConsole(text + "\n");
        }
    }
}