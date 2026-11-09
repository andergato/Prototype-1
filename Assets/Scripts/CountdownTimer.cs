using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float seconds = 3f;
    public TextMeshProUGUI timerText;
    private bool timerStart;

    public static event Action TimerDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            if (seconds > 0.0f)
            {
                Debug.Log("Time left:" + seconds);
                seconds -= Time.deltaTime;
                timerText.text = Math.Round(seconds).ToString();
            }
            if(seconds <= 0.0f)
            {
                print("YAY");
                TimerDone?.Invoke();
                timerStart = false;
            }
        }

    }

    // Called to start timer
    public void StartTimer()
    {
        timerStart = true;
    }
}
