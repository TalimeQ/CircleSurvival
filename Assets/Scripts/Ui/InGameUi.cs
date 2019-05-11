using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUi : MonoBehaviour
{
    [SerializeField]
    GameObject timerUi;
    [SerializeField]
    GameObject loseUi;
    [SerializeField]
    GameObject newRecordText;
    [SerializeField]
    TextMeshProUGUI timeText;

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGameFinished(bool newRecord)
    {
        loseUi.SetActive(true);
        timerUi.SetActive(false);
        if (!newRecord) newRecordText.SetActive(false);
        SetFinalTimeText();
    }

    void SetFinalTimeText()
    {
        float finalTime = GameController.timePassed;
        string minutes = ((int)finalTime / 60).ToString();
        string seconds = (finalTime % 60).ToString("f2");
        string finalValue = minutes + ":" + seconds;
        timeText.SetText("You survived for:\n" + finalValue);
    }
}
