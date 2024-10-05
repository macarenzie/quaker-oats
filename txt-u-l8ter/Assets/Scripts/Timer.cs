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
    [SerializeField] protected float initialTime;
    private float timeRemaining;
    protected bool timerIsRunning;

    // ui
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;


    [SerializeField] protected TMP_Text timerText;
    #endregion

    void Start()
    {
        // set up timer
        timerIsRunning = false;
        timeRemaining = initialTime;

        // hook up the buttons to their methods
        startButton.onClick.AddListener(StartTimer);
        resetButton.onClick.AddListener(ResetTimer);

        // print timer on screen
        DisplayTime(initialTime);
        timerText.gameObject.SetActive(false); // timer hidden until started
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
    }

    #region METHODS
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartTimer()
    {
        timerIsRunning = true;
        timerText.gameObject.SetActive(true);
    }

    void ResetTimer()
    {
        // stop the timer from running
        timerIsRunning = false;

        // reset the time
        timeRemaining = initialTime;

        // update display
        DisplayTime(timeRemaining);
    }
    #endregion
}