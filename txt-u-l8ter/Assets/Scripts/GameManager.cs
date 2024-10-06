using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Phrase phrases;
    public List<string> answers = new List<string>();

    [SerializeField] private TextMeshPro wordPrompt;
    private TextMeshPro resultText;

    [SerializeField] private Timer timer;
    private Input userInput;

    private int wordIndex;

    private bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        wordIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        while (!gameOver)
        {
            //save the initial word displayed after button press
            PressOkay();

            //move on to the next phrase
            NextPhrase();

            if (timer.TimeRemaining == 0)
            {
                EndGame();
            }
        }
    }

    //starts game
    //connects to start call button 
    private void StartGame()
    {
        timer.IsRunning = true;
        wordPrompt.text = phrases.List[wordIndex];
    }

    //ends gmae
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
    }

    private void ShowResults()
    {

    }

    //calculates the accuracy 
    private void CalculateAccuracy()
    {

    }

    //attaching this to Okay button
    public void PressOkay()
    {
        answers[wordIndex] = userInput.Text;
    }
}
