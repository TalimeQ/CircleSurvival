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
        float timeSurvived = GetPlayerHighScore();
        highScoreText.SetText("Longest Time Survived: " + timeSurvived);
    }

    float GetPlayerHighScore()
    {
        //TODO Implement
        return 0.0f;
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
}
