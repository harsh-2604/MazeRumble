using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerMainMenu : MonoBehaviour
{
    public static bool isSinglePlayer = false;
    public void Play()
    {
        SceneManager.LoadScene("Single Player Level 1");
    }

    public void Sensitivity()
    {
        isSinglePlayer = true;
        SceneManager.LoadScene("Set Senstivity");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
