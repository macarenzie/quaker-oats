using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Phrase phrases;
    public List<string> answers = new List<string>();

    private TextMeshPro wordPrompt;
    private TextMeshPro accuracyScore;

    private Timer timer;
    private Input userInput;

    private int wordIndex;

    public int WordIndex
    {
        get { return wordIndex; }
    }

    private bool gameOver;

    //starts game
    //connects to start call button 
    private void StartGame()
    {
        gameOver = false;
        timer.IsRunning = true;
        wordIndex = 0;
        wordPrompt.text = phrases.List[wordIndex];
    }

    // Update is called once per frame
    void Update()
    {
        while (!gameOver)
        {
            if (timer.TimeRemaining == 0)
            {
                EndGame();
            }

            //save the initial word displayed after button press
            PressOkay();

            //move on to the next phrase
            NextPhrase();
        }
    }

    //ends game
    //connects to end call button
    private void EndGame()
    {
        gameOver = true;
        timer.IsRunning = false;

        if (timer.TimeRemaining == 0)
        {
            ShowResults();
        }
    }

    private void NextPhrase()
    {
        //updates the word index and displays the next word
        wordIndex++;
        wordPrompt.text = phrases.List[wordIndex];
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
        int accuracyScore = 0;
        int totalChars = 0;

        //calculate the total amount of chars in the answers array
        foreach (string word in answers)
        {
            for (int i = 0; i < word.Length; i++)
            {
                totalChars++;
            }
        }    

        //grab the answers in the answers list
        foreach (string answer in answers)
        {
            //and int the phrases list
            foreach (string word in phrases.List)
            {
                //itterate through each word's char for both arrays
                for (int i = 0; i < answer.Length; i++)
                {
                    for (int j = 0; j < word.Length; j++)
                    {
                        //compare chars and add to the counter if correct
                        if (answer[i] == word[i])
                        {
                            accurateLetters++;
                        }
                    }
                }
            }
        }

        //calculate percentage
        accuracyScore = accurateLetters / totalChars;

        //returnn string
        return accuracyScore.ToString();
    }

    //attaching this to Okay button
    public void PressOkay()
    {
        answers[wordIndex] = userInput.Text;
        Debug.Log("user input saved into answers!");
    }

    public string GetCurrentPhrase()
    {
        return phrases.List[wordIndex];
    }

    public string GetAccuracyScore()
    {
        return CalculateAccuracy();
    }

}
