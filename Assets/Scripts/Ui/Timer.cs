using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    
    TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
         UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        float timer = GameController.timePassed;
        string minutes = ((int)timer / 60).ToString();
        string seconds = (timer % 60).ToString("f2");
        string updatedValue = minutes + ":" + seconds; 
        textDisplay.SetText("Time passed: " + updatedValue);
    }
}
