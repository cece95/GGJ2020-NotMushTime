using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timers : MonoBehaviour
{
    static float TOTAL_TIME = 180.0f;
    
    float timeLeft;
    float elapsedTime;

    TextMeshProUGUI elapsedTimeText;
    TextMeshProUGUI timeLeftText;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = TOTAL_TIME;
        elapsedTime = 0.0f;

        elapsedTimeText = transform.Find("ElapsedTime").GetComponentInChildren<TextMeshProUGUI>();
        timeLeftText = transform.Find("TimeLeft").GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        elapsedTime += Time.deltaTime;

        timeLeftText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(timeLeft / 60), Mathf.FloorToInt(timeLeft % 60));
        elapsedTimeText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(elapsedTime / 60), Mathf.FloorToInt(elapsedTime % 60));

        if (timeLeft < 0.0f)
        { 
            UnityEvent gameOver = new UnityEvent();
            gameOver.Invoke();
        }
    }
}
