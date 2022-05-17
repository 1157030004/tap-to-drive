using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreGainPerSecond = 0.1f;

    public const string HighestScoreKey = "HighestScore";
    
    float score;

    void Update()
    {
        score += scoreGainPerSecond * Time.deltaTime;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    void OnDestroy() 
    {
        int currentHighestScore = PlayerPrefs.GetInt(HighestScoreKey, 0);

        if(score > currentHighestScore)
        {
            PlayerPrefs.SetInt(HighestScoreKey, Mathf.FloorToInt(score));
        }
    }
}
