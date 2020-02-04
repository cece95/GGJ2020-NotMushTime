using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GotoCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GotoHelpScene()
    {
        SceneManager.LoadScene("Help");
    }

    public void GotoPlayer1Scene()
    {
        // pass 1 player params
        SceneManager.LoadScene("UI");
    }

    public void GotoPlayer2Scene()
    {
        // pass 2 player params
        SceneManager.LoadScene("UI");
    }


}

