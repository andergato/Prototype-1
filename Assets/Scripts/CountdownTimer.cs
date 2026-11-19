using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float seconds = 3f;
    public GameObject timerText;
    [SerializeField] GameObject timerUI;
    private bool timerStart;

    public static event Action TimerDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.SetActive(false);
        timerUI.SetActive(false);
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
                timerText.GetComponent<TextMeshProUGUI>().text = Math.Round(seconds).ToString();
            }
            if(seconds <= 0.0f)
            {
                timerText.SetActive(false);
                timerUI.SetActive(false);
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
        timerText.SetActive(true);
        timerUI.SetActive(true);
    }
}
