using TMPro;
using UnityEngine;

public class Timers : MonoBehaviour
{
    public delegate void TimerDelegate();
    public event TimerDelegate TimerFinished;

    static float TOTAL_TIME = 180.0f;
    
    float timeLeft;
    float elapsedTime;

    bool isRunning = false;

    TextMeshProUGUI elapsedTimeText;
    TextMeshProUGUI timeLeftText;

    private void Awake()
    {
        timeLeft = TOTAL_TIME;
        elapsedTime = 0.0f;

        elapsedTimeText = transform.Find("ElapsedTime").GetComponentInChildren<TextMeshProUGUI>();
        timeLeftText = transform.Find("TimeLeft").GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    public void StartTimer()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning)
        {
            timeLeft -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
        }

        timeLeftText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(timeLeft / 60), Mathf.FloorToInt(timeLeft % 60));
        elapsedTimeText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(elapsedTime / 60), Mathf.FloorToInt(elapsedTime % 60));

        if (isRunning && timeLeft < 0.0f)
        {
            timeLeft = 0.0f;
            elapsedTime = TOTAL_TIME;

            isRunning = false;
            if (TimerFinished != null)
            {
                TimerFinished();
            }
        }
    }
}
