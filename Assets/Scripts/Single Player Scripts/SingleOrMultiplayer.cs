using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleOrMultiplayer : MonoBehaviour
{
    public void SinglePlayer()
    {
        SceneManager.LoadScene("SIngle Player Main Menu");
    }
    
    public void MultiPlayer()
    {
        SceneManager.LoadScene("Enter Username");
    }
}
