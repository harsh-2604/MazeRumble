using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class setSensitivity : MonoBehaviour
{
    public static float sensitivity = 10f;
    public TMP_Text sensText;
    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("OpenForFirstTime") == 10f ? 10f : PlayerPrefs.GetFloat("Sensitivity");
        PlayerPrefs.SetFloat("OpenForFirstTime", 1);
        sensText.text = "Sensitivity: " + sensitivity;
        sensitivity = Mathf.Clamp(sensitivity, 10, 100);
    }
    private void FixedUpdate()
    {
        sensitivity = Mathf.Clamp(sensitivity, 10, 100);
        sensText.text = "Sensitivity: " + sensitivity;
    }
    public void SaveSensitivityAndLoadNextScene()
    { 
        sensitivity = Mathf.Clamp(sensitivity, 10, 100);
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
        if(SinglePlayerMainMenu.isSinglePlayer == true)
        {
            SceneManager.LoadScene("SIngle Player Main Menu");
        }
        else
        {
            PhotonNetwork.LoadLevel("Start Menu");
        }
    }
}