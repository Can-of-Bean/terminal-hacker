using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Papers
{
    public class PapersManager : MonoBehaviour
    {
        public int CurrentPage { get; private set; }
        
        [SerializeField]
        private List<Sprite> m_pages = new List<Sprite>();
        
        [SerializeField]
        private Image m_currentPageDisplay = null!;

        private void Start()
        {
            SetPage(0);
        }

        public void NextPage()
        {
            SetPage(CurrentPage + 1);
        }

        public void PreviousPage()
        {
            SetPage(CurrentPage - 1);
        }

        public void SetPage(int index)
        {
            // overflow clamp
            if (index >= m_pages.Count)
                index = 0;
            else if (index < 0)
                index = m_pages.Count - 1;
            
            Debug.Log(index);

            m_currentPageDisplay.sprite = m_pages[index];
            
            CurrentPage = index;
        }
    }
}