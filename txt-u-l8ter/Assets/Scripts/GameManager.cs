using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Phrase phraseManager;
    [SerializeField] UserInterface uiManager;
    [SerializeField] TextMeshPro timerDisplay;

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

        phraseManager = FindObjectOfType<Phrase>();

        if (phraseManager == null)
        {
            Debug.LogError("Phrase Manager not found in the scene!");
            return;
        }

        currentPhrase = phraseManager.List[Random.Range(0, phraseManager.List.Count + 1)];

        uiManager.DisplayPhrase(currentPhrase);

        // if start button is clicked
        gameActive = true;
        timer = 60f;
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
