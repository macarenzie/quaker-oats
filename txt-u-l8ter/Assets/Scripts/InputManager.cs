using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] TMP_InputField userInputField;  // Input field to display the text
    private string finalizedString = "";             // Finalized string across all buttons

    // List of InputDummy scripts for each button (2, 3, 4, etc.)
    [SerializeField] List<InputDummy> inputDummies;

    // This function will be called by each button's OnClick event
    public void OnButtonClick(int buttonIndex)
    {
        // Access the corresponding InputDummy script for the pressed button
        InputDummy dummy = inputDummies[buttonIndex];
        dummy.OnKeyPress(buttonIndex);

        // Get the finalized text from the InputDummy
        string newText = dummy.FinalizedText;

        // Append the new text to the finalized string
        finalizedString += newText;

        // Update the user input field
        userInputField.text = finalizedString;
    }
}
