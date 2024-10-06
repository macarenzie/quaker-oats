using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    #region FIELDS

    [SerializeField] private string[] letters;
    private int letterIndex = 0;
    // private float pressBuffer = 0.5f;

    // ui
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button letterButton;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        letterButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region METHODS

    private void OnButtonClick()
    {
        letterIndex = (letterIndex + 1) % letters.Length;
        OutputLetter(letters[letterIndex]);
    }

    private void OutputLetter(string letter)
    {
        displayText.text += letter;
    }

    #endregion
}
