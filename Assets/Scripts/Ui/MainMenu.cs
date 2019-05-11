using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreText;

    void Start()
    {
        string timeSurvived = GetPlayerHighScore();
        highScoreText.SetText("Longest Time Survived: " + timeSurvived);
    }

    string GetPlayerHighScore()
    {
        float highScore =  PlayerPrefs.GetFloat("Highscore", 0.0f);
        string minutes = ((int)highScore / 60).ToString();
        string seconds = (highScore % 60).ToString("f2");
        string updatedValue = minutes + ":" + seconds;
        return updatedValue;
    }

    public void ButtonQuit()
    {
        print("Qutting!");
        Application.Quit();
    }

    public void ButtonStart()
    {
        print("Started!");
        SceneManager.LoadScene(1);
    }
}
