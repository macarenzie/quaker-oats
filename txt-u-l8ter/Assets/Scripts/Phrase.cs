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
    protected List<string> phraseList = new List<string>();

    #endregion

    #region PROPERTIES
    public List<string> List
    {
        get { return phraseList; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadFile();
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
