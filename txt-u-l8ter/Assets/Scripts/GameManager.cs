using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Phrase phrases;
    public List<string> answers = new List<string>();

    [SerializeField] private TextMeshPro wordPrompt;
    private TextMeshPro accuracyScore;

    [SerializeField] private Timer timer;
    private InputDummy userInput;

    private int wordIndex;

    private bool gameOver;

    private void Start()
    {
        wordPrompt.text = "Press Start to begin!";
        timer.IsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (timer.TimeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    //starts game
    //connects to start call button 
    public void StartGame()
    {
        wordIndex = 0;
        answers.Clear();
        gameOver = false;
        timer.IsRunning = true;
        wordPrompt.text = phrases.List[wordIndex];
        Debug.Log("Game started");
    }

    //ends gmae
    //connects to end call button
    public void EndGame()
    {
        gameOver = true;
        timer.IsRunning = false;
        Debug.Log("Game started");
    }

    private void NextPhrase()
    {
        //save the initial word displayed after button press
        PressOkay();

        //updates the word index and displays the next word
        wordIndex++;

        if (wordIndex < phrases.List.Count)
        {
            wordPrompt.text = phrases.List[wordIndex];
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

        for (int i = 0; i < answers.Count && i < phrases.List.Count; i++)
        {
            string answer = answers[i];
            string phrase = phrases.List[i];

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

    //attaching this to Okay button
    public void PressOkay()
    {
        answers[wordIndex] = userInput.FinalizedText;
        Debug.Log("user input saved into answers!");
    }
}
