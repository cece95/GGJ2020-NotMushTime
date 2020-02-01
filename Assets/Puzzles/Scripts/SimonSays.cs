using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Timers;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private KeyCode Red1, Red2, Blue1, Blue2, Yellow1, Yellow2, Green1, Green2;
    public SpriteRenderer red, blue, green, yellow;//0,1,2,3
    int[] puzzle = new int[5];
    bool pass = false;
    bool timefail = false;
    public bool hit = false;
    System.Random random = new System.Random();
    private static System.Timers.Timer aTimer;
    int puzzleposition = 0;
    int puztrack = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetTimer();
        red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;
        for (int i = 0; i < 5; i++)
        {
            puzzle[i] = RandomNumber(0, 4);


        }
        Debug.Log(puzzle[0]);
        Debug.Log(puzzle[1]);
        Debug.Log(puzzle[2]);
        Debug.Log(puzzle[3]);
        Debug.Log(puzzle[4]);
    }
    


    // Update is called once per frame
    void Update()
    {
        PuzzleGo(puzzle);
        SimonInput(puzzle);
        
        PuzzlePass(puzzle);
    }
    public int RandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }

    void wait(double x)
    {
        DateTime t = DateTime.Now;
        DateTime tf = DateTime.Now.AddSeconds(x);

        while (t < tf)
        {
            t = DateTime.Now;
        }
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(5000);
        // Hook up the Elapsed event for the timer.
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private void PuzzleGo(int[]puz)
    {
    

            //hit = false;
            if (puz[puzzleposition] == 0)
            {
                red.enabled = true;
            }
            if (puz[puzzleposition] == 1)
            {
                blue.enabled = true;
            }
            if (puz[puzzleposition] == 2)
            {
                green.enabled = true;
            }
            if (puz[puzzleposition] == 3)
            {
                yellow.enabled = true;
            }

        
        
    }

    private void SimonInput(int[] puz)
    {
        if (puz[puzzleposition] == 0)
        {

            if (Input.GetKeyDown(Red1) || Input.GetKeyDown(Red2))
            {
                hit = true;
                red.enabled = false;
                
            }
            /*else
            {
                hit = false;
            }*/
        }
        else if (puz[puzzleposition] == 1)
        {
            blue.enabled = true;
            if (Input.GetKeyDown(Blue1) || Input.GetKeyDown(Blue2))
            {
                hit = true;
                blue.enabled = false;
               
            }
            /*else
            {
                hit = false;
            }*/
        }
        else if (puz[puzzleposition] == 2)
        {
            green.enabled = true;
            if (Input.GetKeyDown(Green1) || Input.GetKeyDown(Green2))
            {
                hit = true;
                green.enabled = false;
                
            }
            /*else
            {
                hit = false;
            }*/
        }
        else if (puz[puzzleposition] == 3)
        {
            yellow.enabled = true;
            if (Input.GetKeyDown(Yellow1) || Input.GetKeyDown(Yellow2))
            {
                hit = true;
                yellow.enabled = false;
            }
            /*else
            {
                hit = false;
            }*/
        }
    }

    private void OnTimedEvent(System.Object source, ElapsedEventArgs e)
    {
        //puzzleposition++;
        red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;
        //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
        if(hit == false)
        {
            puzzleposition++;
            pass = false;
        }
        else
        {
            pass = true;
        }
        Progression();
    }

    private void Progression()
    {
        if(pass == false)
        {
            red.enabled = false;
            blue.enabled = false;
            green.enabled = false;
            yellow.enabled = false;
            //timefail = true;
            puzzleposition = 0;
            Debug.Log("Fail!");
        }
        else
        {
            Debug.Log("Pass!");
            red.enabled = false;
            blue.enabled = false;
            green.enabled = false;
            yellow.enabled = false;
            puzzleposition++;
            pass = false;
            hit = false;
        }
    }

    private void PuzzlePass(int[] puz)
    {
        if(puzzleposition == puz.Length)
        {
            //pass = true;
            red.enabled = false;
            blue.enabled = false;
            green.enabled = false;
            yellow.enabled = false;
            puztrack++;
        }
    }
}
