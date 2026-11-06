using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float seconds = 0.0f;
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
            while (seconds > 0.0f)
            {
                seconds -= Time.deltaTime;

                timerText.text = seconds.ToString();
            }
        }
        TimerDone?.Invoke();
    }

    public void StartTimer()
    {
        timerStart = true;
    }
}
