using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    static float TOTAL_TIME = 180.0f;
    
    float timeLeft;
    float testingTime;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = TOTAL_TIME;
        testingTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        testingTime += Time.deltaTime;

        if (timeLeft < 0.0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }
}
