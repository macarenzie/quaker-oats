using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] TMP_InputField userInputField;  // Input field to display the text
    private string finalizedString = "";             // Finalized string across all buttons

    public string FinalizedString
    {
        get { return finalizedString;}
    }

    // List of InputDummy scripts for each button (2, 3, 4, etc.)
    [SerializeField] List<InputDummy> inputDummies;

    public void OnOKPress()
    {
        for (int i = 0; i < inputDummies.Count; i++)
        {
            if (inputDummies[i].IsClicked)
            {
                userInputField.text = "";   //Empties it out to make sure that letters from all the buttons are added
                finalizedString += inputDummies[i].LastLetter; // Finalize the current letter
                inputDummies[i].IsClicked = false;
            }
        }

        userInputField.text = finalizedString; // Update the user input field
    }
}
