using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Purpose: Manages UI elements like the keypad, phrase display, start/stop buttons, and results (time and accuracy)
/// Author(s): Lisa Pham
/// </summary>
public class UserInterface : MonoBehaviour
{
    [SerializeField] TextMeshPro phraseDisplay;
    [SerializeField] TextMeshPro resultDisplay;
    [SerializeField] GameObject keypad;
    // refer to game class

    // Start is called before the first frame update
    void Start()
    {
         // Get the screen width and height
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Display the screen size in the console
        Debug.Log("Screen Width: " + screenWidth);
        Debug.Log("Screen Height: " + screenHeight);

        // Calculate the aspect ratio
        float aspectRatio = (float)screenWidth / screenHeight;
        Debug.Log("Aspect Ratio: " + aspectRatio);
    }

    // Update is called once per frame
    void Update()
    {
        ShowResults(12, 80.32f);
        DisplayPhrase("Chicken Wing");
        
        // if STOP is pressed, reset
    }

    public void DisplayPhrase(string phrase)
    {
        phraseDisplay.text = phrase;
    }

    /// <summary>
    /// show the user the amounts of words done and user's accuracy
    /// </summary>
    /// <param name="words">number of words attempted</param>
    /// <param name="accuracy">how correct each phrase was spelled</param>
    public void ShowResults(int words, float accuracy)
    {
        resultDisplay.text = $"Words Done: {words:F2} seconds\nAccuracy: {accuracy:P2}";
    }

    /// <summary>
    /// when STOP is pressed, reset values to begin again
    /// </summary>
    public void ResetUI()
    {
        phraseDisplay.text = "";
        resultDisplay.text = "";
    }
}
