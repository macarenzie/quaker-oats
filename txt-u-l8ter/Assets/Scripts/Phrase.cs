using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
/// Class: Phrase
/// Purpose: handles all parts of generating the phrases
/// Author(s): McKenzie Lam
/// </summary>
public class Phrase : MonoBehaviour
{
    #region FIELDS
    protected List<string> phraseList;

    // testing purposes
    [SerializeField] TMP_Text displayText;
    private int phraseIndex;
    #endregion

    #region PROPERTIES
    protected int Count
    {
        get { return phraseList.Count; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadFile();
        phraseIndex = 0;

        if (displayText != null)
        {
            displayText.text = phraseList[phraseIndex] + "\nList count = " + phraseList.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region METHODS

    void LoadFile()
    {
        string filePath = Path.Combine(Application.dataPath, "Assets", "Phrases.txt");

        if (File.Exists(filePath))
        {
            phraseList = new List<string>(File.ReadAllLines(filePath));
            foreach (string phrase in phraseList)
            {
                Debug.Log(phrase);
            }
        }
        else
        {
            Debug.Log("File not found at: " + filePath);
        }
    }
    #endregion
}
