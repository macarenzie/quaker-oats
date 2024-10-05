//Same code but with using input field instead

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Script: Input
/// Purpose: Handles the button input from the user
/// Author(s): Vaibhavy Darshan, McKenzie Lam
/// Bugs: Space does not work at times
/// </summary>
public class AnotherInput : MonoBehaviour
{
    [SerializeField] TMP_InputField userInputField; // Changed to TMP_InputField
    [SerializeField] float typingDelay = 1.35f; // Delay to finalize the letter
    Dictionary<int, List<char>> keypadLetters = new Dictionary<int, List<char>>();
    [SerializeField] int currentKey = -1;
    int letterIndex = -1;
    float lastKeyPressTime;

    void Start()
    {
        // Initialize the keypad dictionary
        keypadLetters[0] = new List<char> { ' ' };
        keypadLetters[2] = new List<char> { 'a', 'b', 'c' };
        keypadLetters[3] = new List<char> { 'd', 'e', 'f' };
        keypadLetters[4] = new List<char> { 'g', 'h', 'i' };
        keypadLetters[5] = new List<char> { 'j', 'k', 'l' };
        keypadLetters[6] = new List<char> { 'm', 'n', 'o' };
        keypadLetters[7] = new List<char> { 'p', 'q', 'r', 's' };
        keypadLetters[8] = new List<char> { 't', 'u', 'v' };
        keypadLetters[9] = new List<char> { 'w', 'x', 'y', 'z' };
    }

    // Function to handle keypresses
    public void OnKeyPress(int key)
    {
        if (keypadLetters.ContainsKey(key))
        {
            float timeSinceLastPress = Time.time - lastKeyPressTime;

            // If the same key is pressed within the delay window, cycle through letters
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
                letterIndex = 0; // Reset to the first letter of the new key
            }

            // Display the currently selected letter
            DisplayCurrentLetter(keypadLetters[currentKey][letterIndex]);

            lastKeyPressTime = Time.time; // Update time of the last key press
        }
    }

    // Function to finalize the current letter if delay has passed
    void Update()
    {
        if (currentKey != -1 && Time.time - lastKeyPressTime >= typingDelay && letterIndex != -1)
        {
            // Add the current letter to the input field and reset for the next keypress
            AppendLetterToInput(keypadLetters[currentKey][letterIndex]);
            currentKey = -1; // Reset current key
        }
    }

    // Display the currently selected letter (replaces the last character)
    void DisplayCurrentLetter(char currentLetter)
    {
        // Remove the last character (if it exists), and display the current letter being cycled
        if (userInputField.text.Length > 0)
        {
            userInputField.text = userInputField.text.Substring(0, userInputField.text.Length - 1);
        }

        userInputField.text += currentLetter; // Display the current letter

        //Debug.Log(userInputField.text);
    }

    // Append the finalized letter to the input field
    void AppendLetterToInput(char letter)
    {
        userInputField.text += letter; // Append the letter to the input field
    }
}
