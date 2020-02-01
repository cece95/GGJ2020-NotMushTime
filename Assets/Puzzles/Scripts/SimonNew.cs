using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class SimonNew : MonoBehaviour
{
    [SerializeField] private KeyCode Red1, Red2, Blue1, Blue2, Yellow1, Yellow2, Green1, Green2;
    public SpriteRenderer red, blue, green, yellow;//0,1,2,3
    int[] puzzle = new int[5];
    bool play = false;
    bool displayswitch = true;
    public bool hit = false;
    System.Random random = new System.Random();
    private static System.Timers.Timer aTimer, aTimer2;
    int puzzleposition = 0;
    int puztrack = 0;
    int[] answer = new int[5];
    int movecount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetTimer();
        /*red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;*/
        red.color = new Color(1f, 1f, 1f, 0f);
        blue.color = new Color(1f, 1f, 1f, 0f);
        green.color = new Color(1f, 1f, 1f, 0f); 
        yellow.color = new Color(1f, 1f, 1f, 0f);
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
        //Clearer();
    }
    public int RandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(500);
        // Hook up the Elapsed event for the timer.
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private void OnTimedEvent(System.Object source, ElapsedEventArgs e)
    {
        if (play == false)
        {
            if (displayswitch == false)
            {
                puzzleposition++;
                displayswitch = true;
            }
            else
            {
                displayswitch = false;
            }
            if (puzzleposition == puzzle.Length)
            {
                puzzleposition = 0;
                play = true;
                displayswitch = false;
            }
        }
        //Clearer();
        /*red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;*/
        //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
        /*if (hit == false)
        {
            puzzleposition++;
            pass = false;
        }
        else
        {
            pass = true;
        }*/
        //Progression();
        //PuzzleGo(puzzle);
    }

   

    
    private void PuzzleGo(int[] puz)
    {


        //hit = false;
        if (displayswitch == true)
        {
            if (puz[puzzleposition] == 0)
            {
                red.color = new Color(1f, 0f, 0f, 1f);
                blue.color = new Color(1f, 1f, 1f, 0f);
                green.color = new Color(1f, 1f, 1f, 0f);
                yellow.color = new Color(1f, 1f, 1f, 0f);
            }
            if (puz[puzzleposition] == 1)
            {
                blue.color = new Color(0f, 0f, 1f, 1f);
                red.color = new Color(1f, 1f, 1f, 0f);
                green.color = new Color(1f, 1f, 1f, 0f);
                yellow.color = new Color(1f, 1f, 1f, 0f);
            }
            if (puz[puzzleposition] == 2)
            {
                green.color = new Color(0f, 1f, 0f, 1f);
                red.color = new Color(1f, 1f, 1f, 0f);
                blue.color = new Color(1f, 1f, 1f, 0f);
                yellow.color = new Color(1f, 1f, 1f, 0f);
            }
            if (puz[puzzleposition] == 3)
            {
                yellow.color = new Color(1f, 1f, 0f, 1f);
                red.color = new Color(1f, 1f, 1f, 0f);
                blue.color = new Color(1f, 1f, 1f, 0f);
                green.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else
        {
            red.color = new Color(1f, 1f, 1f, 0f);
            blue.color = new Color(1f, 1f, 1f, 0f);
            green.color = new Color(1f, 1f, 1f, 0f);
            yellow.color = new Color(1f, 1f, 1f, 0f);
        }

    }

    private void SimonInput(int[] puz) 
    {
        if (play == true)
        {
            if (Input.GetKeyDown(Red1) || Input.GetKeyDown(Red2))
            {
                answer.SetValue(0, movecount);
                movecount++;
            }
            if (Input.GetKeyDown(Blue1) || Input.GetKeyDown(Blue2))
            {
                answer.SetValue(1, movecount);
                movecount++;
            }
            if (Input.GetKeyDown(Green1) || Input.GetKeyDown(Green2))
            {
                answer.SetValue(3, movecount);
                movecount++;
            }
            if (Input.GetKeyDown(Yellow1) || Input.GetKeyDown(Yellow2))
            {
                answer.SetValue(2, movecount);
                movecount++;
            }
            if (movecount == answer.Length)
            {
                //play = false;
                Debug.Log("input test");
                wincondition(puzzle, answer);
            }
        }
    }
    private void wincondition(int[] puz, int[] ans)
    {
        bool passer = true;
        for (int i = 0; i<ans.Length; i++)
        {
            ans[i] = ans[i];
            puz[i] = puz[i];
            if (ans[i] != puz[i])
            {
                passer = false;
            }
        }
        if(passer == true)
        {
            Debug.Log("Win");
            puztrack++;
            puzzleposition = 0;
            movecount = 0;
            displayswitch = true;
            play = false;
        }
        else if (passer == false)
        {
            Debug.Log("you suck!");
            puzzleposition = 0;
            movecount = 0;
            displayswitch = true;
            play = false;
            /*for (int j = 0; j < ans.Length; j++)
            {
                Array.Clear(ans, j, ans.Length);
            }*/
        }
    }
}
