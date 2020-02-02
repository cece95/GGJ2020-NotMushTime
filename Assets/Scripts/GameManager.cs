using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { if (instance == null) { instance = FindObjectOfType<GameManager>(); } if (instance == null) { GameObject go = new GameObject(); go.name = "Game Manager"; instance = go.AddComponent<GameManager>(); } return instance; } }
    private static GameManager instance;

    [SerializeField]
    private PuzzleRenderer puzzlePortal;

    private List<PuzzleRenderer> puzzleRenderers;

    private Player[] players;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        puzzlePortal = Resources.Load<PuzzleRenderer>("Prefabs/PuzzlePortal");

        SetupPlayers();
        InitializeGame();
    }

    void InitializeGame()
    {
        // Play dialogue
    }

    void OnDialogueFinished()
    {
        // Start timer
        // Enable player movement
    }

    public void StartPuzzle(Player[] players, Puzzle puzzleToStart, Vector3 portalPosition, Vector3 portalSize)
    {
        // Instantiate puzzle

        // Disable player movement
        foreach(Player player in players)
        {
            player.SetAllowMovement(false);
        }

        // Display puzzle
        PuzzleRenderer newPuzzleRenderer = Instantiate(puzzlePortal);
        newPuzzleRenderer.transform.position = portalPosition;
        newPuzzleRenderer.transform.GetChild(0).localScale = portalSize;
        newPuzzleRenderer.SetPuzzleToRender(puzzleToStart);

        puzzleToStart.gameObject.SetActive(true);
        puzzleToStart.MyRenderer = newPuzzleRenderer;
        puzzleToStart.OnPuzzleCompleted += OnPuzzleEnded;

        puzzleToStart.StartPuzzle(players);
    }

    void OnPuzzleEnded(Puzzle puzzle)
    {
        UnbindEvents(puzzle);

        // Enable player movement
        foreach (Player player in puzzle.Players)
        {
            player.SetAllowMovement(true);
        }

        // Fade out puzzle
        puzzle.MyRenderer.FadeOut();
        puzzle.gameObject.SetActive(false);
    }


    void OnPuzzleQuit(Puzzle puzzle)
    {
        OnPuzzleEnded(puzzle);

        //TODO: Reenable puzzle?
    }

    void BindEvents(Puzzle puzzle)
    {
        puzzle.OnPuzzleCompleted += OnPuzzleEnded;
        puzzle.OnPuzzleQuit += OnPuzzleQuit;
    }

    void UnbindEvents(Puzzle puzzle)
    {
        puzzle.OnPuzzleCompleted -= OnPuzzleEnded;
        puzzle.OnPuzzleQuit -= OnPuzzleQuit;
    }

    void EnablePlayerMovement(int playerId, bool movementEnabled)
    {
        if(playerId < players.Length)
        {
            players[playerId].SetAllowMovement(movementEnabled);
        }
    }

    void SetupPlayers()
    {
        players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if(player.name.Contains("Mush"))
            {
                player.SetPlayerController(PlayerInput.Instance.Player1);
            }
            else
            {
                player.SetPlayerController(PlayerInput.Instance.Player2);
            }
        }
    }
}
