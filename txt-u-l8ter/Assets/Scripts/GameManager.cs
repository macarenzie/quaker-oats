using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Phrase phraseManager;
    public List<string> answers = new List<string>();

    [SerializeField] private TextMeshPro wordPrompt;
    [SerializeField] private TextMeshPro accuracyScore;

    [SerializeField] TextMeshPro timerDisplay;
    private float timer = 60f;
    private InputDummy userInput;
    private InputManager inputManager;

    private int wordIndex;
    private int correctLetter;

    private bool gameActive;

    public int WordIndex
    {
        get { return wordIndex; }
    }


    private void Start()
    {
        phraseManager = FindObjectOfType<Phrase>();

        if (phraseManager == null)
        {
            Debug.LogError("Phrase Manager not found in the scene!");
            return;
        }

        userInput = FindObjectOfType<InputDummy>();

        if (userInput == null)
        {
            Debug.LogError("InputDummy component not found in the scene!");
            return;
        }

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        while (gameActive)
        {
            timer -= Time.deltaTime;
            timerDisplay.text = timer.ToString("F2");

            if (timer == 0)
            {
                EndGame();
            }

            NextPhrase();
        }
    }

    //starts game
    //connects to start call button 
    public void StartGame()
    {
        wordIndex = 0;
        correctLetter = 0;
        answers.Clear();
        gameActive = true;
        wordPrompt.text = phraseManager.List[wordIndex];
        Debug.Log("Game started");
    }

    //ends gmae
    //connects to end call button
    private void EndGame()
    {
        gameActive = false;
        Reset();
        accuracyScore.text = CalculateAccuracy();
        Debug.Log("Game started");
    }

    public void Reset()
    {
        gameActive = false;
        wordPrompt.text = "Press Start to begin!";
        wordIndex = 0;
        correctLetter = 0;
        answers.Clear();
        timer = 60f;
    }

    private void NextPhrase()
    {
        //save the initial word displayed after button press
        inputManager.OnOKPress();

        //updates the word index and displays the next word
        wordIndex++;

        if (wordIndex < phraseManager.List.Count)
        {
            wordPrompt.text = phraseManager.List[wordIndex];
        }
        else
        {
            EndGame();
        }

        Debug.Log("moved onto next phrase");
    }

    //assign the calculated accuracy to the textmesh
    private void ShowResults()
    {
        accuracyScore.text = CalculateAccuracy();
        Debug.Log("accuracy score reported");
    }

    //calculates the accuracy 
    private string CalculateAccuracy()
    {
        int accurateLetters = 0;
        int totalLetters = 0;

        for (int i = 0; i < answers.Count && i < phraseManager.List.Count; i++)
        {
            string answer = answers[i];
            string phrase = phraseManager.List[i];

            totalLetters += Mathf.Max(answer.Length, phrase.Length);

            for (int j = 0; j < Mathf.Min(answer.Length, phrase.Length); j++)
            {
                if (answer[j] == phrase[j])
                {
                    accurateLetters++;
                }
            }
        }

        float accuracy = (float)accurateLetters / totalLetters * 100f;
        return accuracy.ToString("F2") + "%";
    }

    //void TrackAccuracy(string input, string currentPhrase)
    //{
    //    int correctCount = Mathf.Min(input.Length, currentPhrase.Length);
    //    for (int i = 0; i < correctCount; i++)
    //    {
    //        if (input[i] == currentPhrase[i])
    //        {
    //            correctLetter++;
    //        }
    //    }
    //}

    //attaching this to Okay button
    //public void PressOkay()
    //{
    //    answers[wordIndex] = userInput.FinalizedText;
    //    Debug.Log("user input saved into answers!");
    //}

    public string GetCurrentPhrase()
    {
        return phraseManager.List[wordIndex];
    }

    public string GetAccuracyScore()
    {
        return CalculateAccuracy();
    }
}