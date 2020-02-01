using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private KeyCode Red1, Red2, Blue1, Blue2, Yellow1, Yellow2, Green1, Green2;
    public SpriteRenderer red, blue, green, yellow;
    int[] puzzle;
    bool pass = false;
    bool timefail = false;
    bool hit = false;
    private static System.Timers.Timer aTimer;
    int puzzleposition = 0;
    // Start is called before the first frame update
    void Start()
    {
        red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;
        for (int i = 0; i<6; i++)
        {
            puzzle[i] = RandomNumber(0, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    private static void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
        if(hit == false)
        {
            timefail = true;
        }

    }
}
