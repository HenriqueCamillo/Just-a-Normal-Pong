using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunnerScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private float score;
    float Score
    {
        get => score;
        set 
        {
            score = value;
            scoreText.SetText("Score: " + ((int)score).ToString());
        }
    }
    [SerializeField] float multiplier;
    bool inGame;

    private void OnEnable()
    {
        inGame = true;
        RunnerObjectGenerator.OnReset += Reset; 
        RunnerObjectGenerator.OnDeath += Death; 
    }

    private void OnDisable()
    {
        RunnerObjectGenerator.OnReset -= Reset; 
        RunnerObjectGenerator.OnDeath -= Death; 
    }

    private void Reset()
    {
        Score = 0;
        inGame = true;
    }

    private void Death()
    {
        inGame = false;
    }

    private void Update()
    {
        if (inGame)
        {
            Score += Time.deltaTime * multiplier;
        }
    }
}
