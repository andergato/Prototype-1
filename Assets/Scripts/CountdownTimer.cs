using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float seconds = 3f;
    private float secondsElapsed;
    public GameObject timerText;
    [SerializeField] GameObject timerUI;
    private bool timerStart;

    public static event Action TimerDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (timerText)
        {
            timerText.SetActive(false);
            timerUI.SetActive(false);
        }
        timerStart = false;
        secondsElapsed = seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            
            if (secondsElapsed > 0.0f)
            {
                Debug.Log("Time left:" + secondsElapsed);
                secondsElapsed -= Time.deltaTime;
                if (timerText) timerText.GetComponent<TextMeshProUGUI>().text = Math.Round(secondsElapsed).ToString();
            }
            if(secondsElapsed <= 0.0f)
            {
                if (timerText)
                {
                    timerText.SetActive(false);
                    timerUI.SetActive(false);
                }
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
        if (timerText)
        {
            timerText.SetActive(true);
            timerUI.SetActive(true);
        }
    }
}
