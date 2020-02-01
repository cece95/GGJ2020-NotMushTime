using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;


public class SimonNew : MonoBehaviour
{
    //[SerializeField] private KeyCode Red1, Red2, Blue1, Blue2, Yellow1, Yellow2, Green1, Green2;
    public SpriteRenderer red, blue, green, yellow;//0,1,2,3
    int[] puzzle = new int[4];
    int[] answer = new int[4];
    int[] puzzle1 = new int[5];
    int[] answer1 = new int[5];
    int[] puzzle2 = new int[6];
    int[] answer2 = new int[6];
    bool play = false;
    bool displayswitch = true;
    public bool hit = false;
    System.Random random = new System.Random();
    private static System.Timers.Timer aTimer, aTimer2;
    int puzzleposition = 0;
    int puztrack = 0;
    public int timervalue = 800;
    int movecount = 0;
    float timecounter = 0.0f;
    float timestore;
    Component PI;

    PlayerController playerController, playerController1;

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerInput.Instance.Player1;
        playerController1 = PlayerInput.Instance.Player2;
        

        SetTimer();
        /*red.enabled = false;
        blue.enabled = false;
        green.enabled = false;
        yellow.enabled = false;*/
        red.color = new Color(1f, 1f, 1f, 0f);
        blue.color = new Color(1f, 1f, 1f, 0f);
        green.color = new Color(1f, 1f, 1f, 0f); 
        yellow.color = new Color(1f, 1f, 1f, 0f);
        for (int i = 0; i < 4; i++)
        {
            puzzle[i] = RandomNumber(0, 4);
        }
        for (int i = 0; i < 5; i++)
        {
            puzzle1[i] = RandomNumber(0, 4);
        }
        for (int i = 0; i < 6; i++)
        {
            puzzle2[i] = RandomNumber(0, 4);
        }
        Debug.Log(puzzle[0]);
        Debug.Log(puzzle[1]);
        Debug.Log(puzzle[2]);
        Debug.Log(puzzle[3]);

        Debug.Log("puzzle 2");

        Debug.Log(puzzle1[0]);
        Debug.Log(puzzle1[1]);
        Debug.Log(puzzle1[2]);
        Debug.Log(puzzle1[3]);
        Debug.Log(puzzle1[4]);

        Debug.Log("puzzle 3");

        Debug.Log(puzzle2[0]);
        Debug.Log(puzzle2[1]);
        Debug.Log(puzzle2[2]);
        Debug.Log(puzzle2[3]);
        Debug.Log(puzzle2[4]);
        Debug.Log(puzzle2[5]);

    }

    // Update is called once per frame
    void Update()
    {
        timecounter += Time.deltaTime;
        if (puztrack == 0)
        {
            PuzzleGo(puzzle);
            SimonInput(puzzle, answer);
        }
        if (puztrack == 1)
        {
            PuzzleGo(puzzle1);
            SimonInput(puzzle1, answer1);
        }
        if (puztrack == 2)
        {
            PuzzleGo(puzzle2);
            SimonInput(puzzle2, answer2);
        }
    }
    public int RandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(timervalue);
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
            if (puztrack == 0)
            {
                if (puzzleposition == puzzle.Length)
                {
                    puzzleposition = 0;
                    play = true;
                    displayswitch = false;
                }
            }
            if (puztrack == 1)
            {
                if (puzzleposition == puzzle1.Length)
                {
                    puzzleposition = 0;
                    play = true;
                    displayswitch = false;
                }
            }
            if (puztrack == 2)
            {
                if (puzzleposition == puzzle2.Length)
                {
                    puzzleposition = 0;
                    play = true;
                    displayswitch = false;
                }
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

    private void SimonInput(int[] puz, int[] ans) 
    {
        
        if (play == true)
        {
            if (playerController.IsRedDown() || playerController1.IsRedDown())
            {
                timestore = timecounter;
                ans.SetValue(0, movecount);
                movecount++;
            }
            if (playerController.IsBlueDown() || playerController1.IsBlueDown())
            {
                timestore = timecounter;
                ans.SetValue(1, movecount);
                movecount++;
            }
            if (playerController.IsGreenDown() || playerController1.IsGreenDown())
            {
                timestore = timecounter;
                ans.SetValue(3, movecount);
                movecount++;
            }
            if (playerController.IsYellowDown() || playerController1.IsYellowDown())
            {
                timestore = timecounter;
                ans.SetValue(2, movecount);
                movecount++;
            }
            if (movecount == ans.Length)
            {
                red.color = new Color(1f, 1f, 1f, 0f);
                blue.color = new Color(1f, 1f, 1f, 0f);
                green.color = new Color(1f, 1f, 1f, 0f);
                yellow.color = new Color(1f, 1f, 1f, 0f);
                //play = false;
                Debug.Log("input test");
                wincondition(puz, ans);
            }
            if (movecount > 0)
            {
                if (ans[movecount-1] == 0&& timecounter - timestore < 0.7f)
                {
                    red.color = new Color(1f, 0f, 0f, 1f);
                    blue.color = new Color(1f, 1f, 1f, 0f);
                    green.color = new Color(1f, 1f, 1f, 0f);
                    yellow.color = new Color(1f, 1f, 1f, 0f);
                }
                else if (ans[movecount-1] == 1 && timecounter - timestore < 0.7f)
                {
                    blue.color = new Color(0f, 0f, 1f, 1f);
                    red.color = new Color(1f, 1f, 1f, 0f);
                    green.color = new Color(1f, 1f, 1f, 0f);
                    yellow.color = new Color(1f, 1f, 1f, 0f);
                }
                else if (ans[movecount-1] == 2 && timecounter - timestore < 0.7f)
                {
                    green.color = new Color(0f, 1f, 0f, 1f);
                    red.color = new Color(1f, 1f, 1f, 0f);
                    blue.color = new Color(1f, 1f, 1f, 0f);
                    yellow.color = new Color(1f, 1f, 1f, 0f);
                }
                else if (ans[movecount-1] == 3 && timecounter - timestore < 0.7f)
                {
                    yellow.color = new Color(1f, 1f, 0f, 1f);
                    red.color = new Color(1f, 1f, 1f, 0f);
                    blue.color = new Color(1f, 1f, 1f, 0f);
                    green.color = new Color(1f, 1f, 1f, 0f);
                }
                else
                {
                    red.color = new Color(1f, 1f, 1f, 0f);
                    blue.color = new Color(1f, 1f, 1f, 0f);
                    green.color = new Color(1f, 1f, 1f, 0f);
                    yellow.color = new Color(1f, 1f, 1f, 0f);
                }
                //if(Input.GetKeyDown(Red1)|| Input.GetKeyDown(Red2) || Input.GetKeyUp(Blue1) || Input.GetKeyUp(Blue2) || Input.GetKeyUp(Green1) || Input.GetKeyUp(Green2) || Input.GetKeyUp(Yellow1) || Input.GetKeyUp(Yellow2))
                //{
                //    red.color = new Color(1f, 1f, 1f, 0f);
                //    blue.color = new Color(1f, 1f, 1f, 0f);
                //    green.color = new Color(1f, 1f, 1f, 0f);
                //    yellow.color = new Color(1f, 1f, 1f, 0f);
                //}
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
        if (puztrack > 2)
        {
            Debug.Log("Ultimate Win!");
        }
    }
}
