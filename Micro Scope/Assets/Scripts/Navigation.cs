using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void HelpScene()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}