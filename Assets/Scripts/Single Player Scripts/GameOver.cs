using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameWonScreen;
    public GameObject gameLostScreen;
    public static bool isWon;

    private void Start()
    {
        gameWonScreen.SetActive(false);
        gameLostScreen.SetActive(false);
        if(isWon) gameWonScreen.SetActive(true);
        else
        {
            gameWonScreen.SetActive(false);
        }

        if(!isWon) gameLostScreen.SetActive(true);
        else
        {
            gameLostScreen.SetActive(false);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("");
    }

    public void Back()
    {
        SceneManager.LoadScene("");
    }

    public void Next()
    {
        SceneManager.LoadScene("");
    }
}
