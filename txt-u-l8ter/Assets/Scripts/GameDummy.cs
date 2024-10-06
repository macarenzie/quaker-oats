using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// Purpose: update variables for user gameplay and determine what to do for player based on input
/// Authors: Lisa Pham
/// </summary>
public class GameDummy : MonoBehaviour
{
    private Phrase phraseManager;
    [SerializeField] UserInterface uiManager;
    [SerializeField] TextMeshPro timerDisplay;

    // Add button references
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;

    private float accuracy;
    private int phrasesCompleted;
    private string currentPhrase;
    private int correctLetter;
    private int totalLettersTyped;
    private float timer = 60f;
    private bool gameActive = false;
    private string playerInput = "";

    // Start is called before the first frame update
    void Start()
    {
        Reset();

        startButton.onClick.AddListener(StartGame);
        stopButton.onClick.AddListener(EndGame);

        phraseManager = FindObjectOfType<Phrase>();

        if (phraseManager == null)
        {
            Debug.LogError("Phrase Manager not found in the scene!");
            return;
        }

        currentPhrase = phraseManager.List[Random.Range(0, phraseManager.List.Count + 1)];

        uiManager.DisplayPhrase(currentPhrase);

        // Disable timer until game is started
        timerDisplay.text = timer.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            timerDisplay.text = timer.ToString("F2");

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    void StartGame()
    {
        gameActive = true;
        timer = 60f;

        currentPhrase = phraseManager.List[Random.Range(0, phraseManager.List.Count)];
        uiManager.DisplayPhrase(currentPhrase);

        Debug.Log("Game Started");
    }

    void EndGame()
    {
        gameActive = false;
        accuracy = CalculateAccuracy();
        uiManager.ShowResults(phrasesCompleted, accuracy);
    }

    void Reset()
    {
        phrasesCompleted = 0;
        correctLetter = 0;
        totalLettersTyped = 0;
        timer = 60f;
        playerInput = "";
        uiManager.ResetUI();
    }

    void HandleKeyPresses(string key)
    {
        if (gameActive)
        {
            playerInput += key;
            totalLettersTyped++;
        }
    }

    void SubmitInput()
    {
        if (gameActive)
        {
            TrackAccuracy(playerInput, currentPhrase);
            currentPhrase = phraseManager.List[Random.Range(0, phraseManager.List.Count + 1)];

            playerInput = "";
            phrasesCompleted++;
        }
    }

    void TrackAccuracy(string input, string currentPhrase)
    {
        int correctCount = Mathf.Min(input.Length, currentPhrase.Length);
        for (int i = 0; i < correctCount; i++)
        {
            if (input[i] == currentPhrase[i])
            {
                correctLetter++;
            }
        }
    }

    float CalculateAccuracy()
    {
        if (totalLettersTyped == 0)
        {
            return 0;
        }
        else
        {
            return (correctLetter / totalLettersTyped) * 100f;
        }
    }

}
