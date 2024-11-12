using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PlayerManagement : MonoBehaviourPunCallbacks
{
    #region Public Components
    public Transform content;
    public PlayerListing playerListing;
    public List<PlayerListing> listingList = new List<PlayerListing>();
    public GameObject startButton;
    public string sceneName;
    public Color[] playerColor = { Color.red, Color.blue, Color.green, Color.yellow };
    #endregion
    private void Start()
    {
        UpdateButtonActive();
    }

    public override void OnJoinedRoom()
    {
        AssignedPlayerColor(PhotonNetwork.LocalPlayer);
        GetCurrentRoomPlayers();
        UpdateButtonActive();

    }

    private void AssignedPlayerColor(Player player)
    {
        int colorIndex;

        if (PhotonNetwork.IsMasterClient && player == PhotonNetwork.MasterClient)
        {
            colorIndex = 0;
        }
        else
        {
            colorIndex = (player.ActorNumber - 1) % playerColor.Length;
        }

        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
        playerProperties["color"] = new Vector3(playerColor[colorIndex].r, playerColor[colorIndex].g, playerColor[colorIndex].b);
        player.SetCustomProperties(playerProperties);
    }


    public void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("Adding Player: " + playerInfo.Value.NickName);
            AddPlayerListing(playerInfo.Value);
        }
    }


    void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            listingList.Add(listing);
            listing.SetPlayerColor(player);
        }
        Debug.Log(player.NickName + ":" + PhotonNetwork.LocalPlayer.UserId);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AssignedPlayerColor(newPlayer);
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("color"))
        {
            var listing = listingList.Find(x => x.Player == targetPlayer);
            if (listing != null)
            {
                listing.SetPlayerColor(targetPlayer);
            }
        }

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int index = 0; index < listingList.Count; index++)
        {
            if (listingList[index].Player == otherPlayer)
            {
                Destroy(listingList[index].gameObject);
                listingList.RemoveAt(index);
            }
        }
        UpdateButtonActive();
    }

    public void UpdateButtonActive()
    {
        if (PhotonNetwork.IsMasterClient) startButton.SetActive(true);
        else
        {
            startButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}