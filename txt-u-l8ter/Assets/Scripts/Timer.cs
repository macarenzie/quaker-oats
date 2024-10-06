using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region FIELDS
    // timer logic
    private float initialTime;
    private float timeRemaining;
    private bool timerIsRunning;

    // ui
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;


    [SerializeField] private TMP_Text timerText;
    #endregion

    public bool IsRunning
    {
        get { return timerIsRunning; }
        set { timerIsRunning = value; }
    }

    public float TimeRemaining
    {
        get { return timeRemaining; }
    }

    void Start()
    {
        // set up timer
        timerIsRunning = false;
        timeRemaining = initialTime;

        // print timer on screen
        DisplayTime(initialTime);
    }

    void Update()
    {
        // if startTimer button clicked
        if (timerIsRunning)
        {
            // start the timer if it hasn't finished yet
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            // determine if the timer ran out
            else
            {
                timerText.text = "2 L8 <3";
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        if (!timerIsRunning)
        {
            // reset the time
            timeRemaining = initialTime;

            // update display
            DisplayTime(timeRemaining);
        }
    }

    #region METHODS
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}