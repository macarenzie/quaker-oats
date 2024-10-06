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
    [SerializeField] TextMeshProUGUI resultDisplay;
    [SerializeField] GameObject keypad;
    // refer to game class
    //[SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // if (gameManager == null)
        // {
        //     gameManager = FindObjectOfType<GameManager>(); 
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if STOP is pressed, reset and show results
        // ShowResults(gameManager.WordIndex, float.Parse(gameManager.GetAccuracyScore()));
       
        // DisplayPhrase(gameManager.GetCurrentPhrase());

        ShowResults(4,12.0f);
        DisplayPhrase("monkey");
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
