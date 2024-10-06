using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Input2 : MonoBehaviour
{
    [SerializeField] TMP_InputField userInputField; // The TMP InputField where text will be displayed
    [SerializeField] Button okButton;
    [SerializeField] float caretBlinkInterval = 0.5f; // Interval for blinking caret
    Dictionary<int, List<char>> keypadLetters = new Dictionary<int, List<char>>();

    private int currentKey = -1;    // Current key pressed
    private int letterIndex = -1;   // Index of the letter being cycled through
    private bool isOk = false; // check if ok button was pressed

    private bool caretVisible = true;  // Control caret visibility
    private Coroutine caretBlinkCoroutine;

    private string finalText = "";  // Text that has been finalized
    private string currentLetter;

    void Start()
    {
        // Initialize the keypad dictionary (retro flip-phone style)
        keypadLetters[0] = new List<char> { ' ' };
        keypadLetters[2] = new List<char> { 'a', 'b', 'c' };
        keypadLetters[3] = new List<char> { 'd', 'e', 'f' };
        keypadLetters[4] = new List<char> { 'g', 'h', 'i' };
        keypadLetters[5] = new List<char> { 'j', 'k', 'l' };
        keypadLetters[6] = new List<char> { 'm', 'n', 'o' };
        keypadLetters[7] = new List<char> { 'p', 'q', 'r', 's' };
        keypadLetters[8] = new List<char> { 't', 'u', 'v' };
        keypadLetters[9] = new List<char> { 'w', 'x', 'y', 'z' };

        // Start the blinking caret coroutine
        //caretBlinkCoroutine = StartCoroutine(BlinkCaret());

        // listener to OK button
        okButton.onClick.AddListener(okButtonClick);
    }

    // Finalize the current letter after the typing delay
    void Update()
    {

    }

    // Function to handle keypresses (simulates pressing a number on a retro keypad)
    public void OnKeyPress(int key)
    {
        if (keypadLetters.ContainsKey(key))
        {
            if (key == currentKey)
            {
                letterIndex = (letterIndex + 1) % keypadLetters[key].Count;
            }
            else{
                currentKey = key;
                letterIndex = 0;
            }
            currentLetter = keypadLetters[currentKey][letterIndex].ToString();
            DisplayCurrentLetterWithCaret();
        }
     
    }

    public void okButtonClick()
    {
        if (currentKey != -1 && letterIndex != -1)
        {
            finalText += currentLetter;
            userInputField.text = finalText + "|";

            currentKey = -1;
            letterIndex = -1;
            currentLetter = "";

            //caretVisible = true;
        }
    }

    // Display the currently selected letter with a blinking caret
    void DisplayCurrentLetterWithCaret()
    {
        if (currentLetter != "")
        {
            userInputField.text = finalText + currentLetter + (caretVisible ? "|" : "");
        }

        // Add the current letter being cycled and the caret
        userInputField.text = finalText + (caretVisible ? "|": "");
    }

    // Coroutine to blink the caret at regular intervals
    /*IEnumerator BlinkCaret()
    {
        while (true)  // Infinite loop for caret blinking
        {
            caretVisible = !caretVisible; // Toggle caret visibility
            yield return new WaitForSeconds(caretBlinkInterval); // Wait for blink interval

            // Refresh the displayed text with the blinking caret
            if (currentKey != -1 && letterIndex != -1)
            {
                DisplayCurrentLetterWithCaret();
            }
            else
            {
                // If no key is pressed, show the finalized text with the caret
                userInputField.text = finalText + (caretVisible ? "|" : "");
            }
        }
    }*/
}
