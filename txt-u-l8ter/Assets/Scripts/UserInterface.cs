using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LISA PHAM

/// <summary>
/// Manages UI elements like the keypad, phrase display, start/stop buttons, and results (time and accuracy)
/// </summary>
public class UserInterface : MonoBehaviour
{
    [SerializeField] Text phraseDisplay;
    [SerializeField] GameObject keypad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPhrase(string phrase)
    {
        phraseDisplay.text = phrase;
    }

    public void ShowResults(float timeTaken, float accuracy)
    {
        resultDisplay.text = $"Time: {timeTaken:F2} seconds\nAccuracy: {accuracy:P2}";
    }

    public void ResetUI()
    {
        phraseDisplay.text = "";
        resultDisplay.text = "";
    }
}
