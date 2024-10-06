//Dummycode using the previous AnotherInput's ideas
//Goal: Handles input from each button SEPARATELY as you add more letters

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputDummy : MonoBehaviour
{
    //[SerializeField] TMP_InputField userInputField; // Changed to TMP_InputField- don't need it as we are
    //having an input manager class to parse all the values from finalizedText property to the input field

    [SerializeField] float typingDelay = 1.35f; // Delay to finalize the letter
    Dictionary<int, List<char>> keypadLetters = new Dictionary<int, List<char>>();
    [SerializeField] int currentKey = -1;
    int letterIndex = -1;
    float lastKeyPressTime;

    // Stores the text before cycling letters (so we only overwrite the last letter), still important here for us to 
    //add consecutive letters
    string finalizedText = "";

    //Read only value for input manager to access it 
    public string FinalizedText
    {
        get { return finalizedText; }
    }

    void Start()
    {
        // Initialize the keypad dictionary
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
                    finalizedText += keypadLetters[currentKey][letterIndex]; // Finalize the last letter
                    Debug.Log(finalizedText);
                    //userInputField.text = finalizedText; // Update the finalized input text
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
            // Finalize the letter and reset for the next keypress
            finalizedText += keypadLetters[currentKey][letterIndex];
            Debug.Log(finalizedText);
            //userInputField.text = finalizedText; // Update the input field
            currentKey = -1; // Reset current key
        }
    }

    // Display the currently selected letter without finalizing it
    void DisplayCurrentLetter(char currentLetter)
    {
        // Display the full finalized text + the current cycling letter
        //userInputField.text = finalizedText + currentLetter;
        Debug.Log(finalizedText + currentLetter);
    }
}
