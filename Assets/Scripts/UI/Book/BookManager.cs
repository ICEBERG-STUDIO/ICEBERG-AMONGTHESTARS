using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class BookManager : MonoBehaviour
{
    #region Variables

    [System.Serializable]
    private struct UIPage
    {
        public GameObject page;
        public string label;
    }

    [Header("Book Pages")]
    [SerializeField] private List<UIPage> _bookPages = new List<UIPage>();

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _textPreviousButton;
    [SerializeField] private TMP_Text _textNextButton;

    private UIPage _currentPage;
    private int _indexMax;
    private int _index = 0;


    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _indexMax = _bookPages.Count - 1;
        _currentPage = _bookPages[0];
    }

    private void ViewPage()
    {
        // update current page
        _currentPage.page.SetActive(false);

        _currentPage = _bookPages[_index];
        _currentPage.page.SetActive(true);

        // update texts
        _title.text = _currentPage.label;
        UpdateButtons();

    }
        
    private void UpdateButtons()
    {
        int previous = (_index-1 < 0) ? _indexMax : _index-1;
        _textPreviousButton.text = _bookPages[previous].label;

        int next = (_index+1 > _indexMax) ? 0 : _index+1;
        _textNextButton.text = _bookPages[next].label;
    }

    #region Buttons
    //Btn
    public void PreviousPage()
    {
        _index = (_index <= 0) ? _indexMax : _index-1;

        ViewPage();
    }

    public void NextPage()
    {
        _index = (_index >= _indexMax) ? 0 : _index+1;

        ViewPage();
    }

    #endregion
}
