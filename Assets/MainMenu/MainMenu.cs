using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highestScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] Button playButton;
    [SerializeField] AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeDuration;

    int energy;

    const string EnergyKey = "Energy";
    const string EnergyReadyKey = "EnergyReady";

    void Start()
    {
        OnApplicationFocus(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) return;

        CancelInvoke();

        int highestScore = PlayerPrefs.GetInt(ScoreSystem.HighestScoreKey, 0);

        highestScoreText.text = $"Highest Score: {highestScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if(energyReadyString == string.Empty) return;

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if(DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play: ({energy})";
    }

    void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play: ({energy})";
    }
    public void Play()
    {

        if(energy < 1) return;

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);

        if(energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene(1);

    }
}
