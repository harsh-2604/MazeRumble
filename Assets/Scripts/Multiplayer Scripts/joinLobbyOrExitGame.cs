using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class joinLobbyOrExitGame : MonoBehaviourPunCallbacks
{
    public string sceneName;
    public TMP_Text playername;

    public void Start()
    {
        playername.text = "Welcome " + ConnectToServer.instance.playerText.text + "!";
    }
    public void JoinLobby()
    {       
        PhotonNetwork.JoinLobby();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Sensitivity()
    {
        SceneManager.LoadScene("Set Senstivity");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
