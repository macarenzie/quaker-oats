using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Input2 : MonoBehaviour
{
    [SerializeField] TMP_InputField userInputField; // The TMP InputField where text will be displayed
    [SerializeField] float typingDelay = 1.0f;      // Time delay to finalize the current letter
    [SerializeField] float caretBlinkInterval = 0.5f; // Interval for blinking caret
    Dictionary<int, List<char>> keypadLetters = new Dictionary<int, List<char>>();

    private int currentKey = -1;    // Current key pressed
    private int letterIndex = -1;   // Index of the letter being cycled through
    private float lastKeyPressTime; // Time of the last key press
    private bool caretVisible = true;  // Control caret visibility
    private Coroutine caretBlinkCoroutine;
    private string finalText = "";  // Text that has been finalized

    void Start()
    {
        // Initialize the keypad dictionary (retro flip-phone style)
        keypadLetters[2] = new List<char> { 'a', 'b', 'c' };
        keypadLetters[3] = new List<char> { 'd', 'e', 'f' };
        keypadLetters[4] = new List<char> { 'g', 'h', 'i' };
        keypadLetters[5] = new List<char> { 'j', 'k', 'l' };
        keypadLetters[6] = new List<char> { 'm', 'n', 'o' };
        keypadLetters[7] = new List<char> { 'p', 'q', 'r', 's' };
        keypadLetters[8] = new List<char> { 't', 'u', 'v' };
        keypadLetters[9] = new List<char> { 'w', 'x', 'y', 'z' };

        // Start the blinking caret coroutine
        caretBlinkCoroutine = StartCoroutine(BlinkCaret());
    }

    // Function to handle keypresses (simulates pressing a number on a retro keypad)
    public void OnKeyPress(int key)
    {
        if (keypadLetters.ContainsKey(key))
        {
            float timeSinceLastPress = Time.time - lastKeyPressTime;

            // If the same key is pressed again within the delay, cycle through letters
            if (key == currentKey && timeSinceLastPress < typingDelay)
            {
                letterIndex = (letterIndex + 1) % keypadLetters[key].Count; // Cycle through letters
            }
            else
            {
                // Finalize the previous letter if delay has not passed
                if (currentKey != -1 && timeSinceLastPress < typingDelay && letterIndex != -1)
                {
                    AppendLetterToInput(keypadLetters[currentKey][letterIndex]);
                }

                // Start typing the new letter
                currentKey = key;
                letterIndex = 0; // Start with the first letter of the new key
            }

            // Display the currently selected letter with the caret
            DisplayCurrentLetterWithCaret(keypadLetters[currentKey][letterIndex]);

            lastKeyPressTime = Time.time; // Update time of the last key press
        }
        Debug.Log("Key pressed: " + key);

    }

    // Finalize the current letter after the typing delay
    void Update()
    {
        if (currentKey != -1 && Time.time - lastKeyPressTime >= typingDelay && letterIndex != -1)
        {
            // Append the current letter and reset keypress tracking
            AppendLetterToInput(keypadLetters[currentKey][letterIndex]);
            currentKey = -1; // Reset key
        }
    }

    // Display the currently selected letter with a blinking caret
    void DisplayCurrentLetterWithCaret(char currentLetter)
    {
        // Remove the blinking caret if it's already visible
        if (userInputField.text.EndsWith("|"))
        {
            userInputField.text = userInputField.text.Substring(0, userInputField.text.Length - 1);
        }

        // Add the current letter being cycled and the caret
        if (finalText.Length > 0)
        {
            userInputField.text = finalText + currentLetter + (caretVisible ? "|" : "");
        }
        else
        {
            userInputField.text = currentLetter.ToString() + (caretVisible ? "|" : "");
        }
    }

    // Append the finalized letter to the input field and move to the next letter
    void AppendLetterToInput(char letter)
    {
        finalText += letter;  // Add the finalized letter to the stored text
        userInputField.text = finalText + "|"; // Show the finalized text with a caret
    }

    // Coroutine to blink the caret at regular intervals
    IEnumerator BlinkCaret()
    {
        while (true)  // Infinite loop for caret blinking
        {
            caretVisible = !caretVisible; // Toggle caret visibility
            yield return new WaitForSeconds(caretBlinkInterval); // Wait for blink interval

            // Refresh the displayed text with the blinking caret
            if (currentKey != -1 && letterIndex != -1)
            {
                DisplayCurrentLetterWithCaret(keypadLetters[currentKey][letterIndex]);
            }
            else
            {
                // If no key is pressed, show the finalized text with the caret
                userInputField.text = finalText + (caretVisible ? "|" : "");
            }
        }
    }
}
