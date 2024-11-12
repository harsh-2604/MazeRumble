using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    #region Public Components
    public string sceneName;
    public TMP_InputField playerText;
    public static ConnectToServer instance;
    public string playerNickname;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
    }

    public void JoinLobby()
    {
        if(playerText == null || string.IsNullOrEmpty(playerText.text))
        {
            Debug.LogError("Enter Valid Username");
            return;
        }
        playerNickname = playerText.text;
        PhotonNetwork.NickName = playerText.text;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined");
        PhotonNetwork.LoadLevel(sceneName);
    }
}
