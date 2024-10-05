using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script: Input
/// Purpose: Handles the button input from the user
/// Author(s): Vaibhavy Darshan, McKenzie Lam
/// </summary>
public class Input : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userInput;
    [SerializeField] float typingDelay = 11.35f; // Delay to finalize the letter
    Dictionary<int, List<char>> keypadLetters = new Dictionary<int, List<char>>();
    [SerializeField] int currentKey = -1;
    int letterIndex = 0;
    float lastKeyPressTime;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the keypad dictionary (e.g., '2' corresponds to 'a', 'b', 'c')
        //keypadLetters[0] = new List<char> {' '};
        //keypadLetters[1] = new List<char> { };
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
                // If it's a different key or time delay has passed, finalize the previous letter
                if (currentKey != -1 && timeSinceLastPress < typingDelay)
                {
                    userInput.text += keypadLetters[currentKey][letterIndex]; // Add finalized letter
                }

                // Start typing the new letter
                currentKey = key;
                letterIndex = 0;
            }

            // display the current letter being cycled through
            userInput.text = keypadLetters[currentKey][letterIndex].ToString();
            lastKeyPressTime = Time.time; // Update time of the last key press
        }
    }

    // Function to finalize the current letter if delay has passed
    void Update()
    {
        if (currentKey != -1 && Time.time - lastKeyPressTime >= typingDelay)
        {
            // Add the current letter to the text and reset for the next keypress
            userInput.text += keypadLetters[currentKey][letterIndex];
            currentKey = -1; // Reset current key
        }
    }
}