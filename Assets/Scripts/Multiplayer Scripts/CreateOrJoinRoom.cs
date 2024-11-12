using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CreateOrJoinRoom : MonoBehaviourPunCallbacks
{
    #region Public Components
    public TextMeshProUGUI createRoomName;
    public string sceneName;
    public Transform scrollViewContent;
    public roomListing RoomListing;
    public List<roomListing> listingList = new List<roomListing>();
    #endregion

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Scene Sync = " + PhotonNetwork.AutomaticallySyncScene);
    }

    public void Back()
    {
        PhotonNetwork.LoadLevel("Start Menu");
    }
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (createRoomName == null || string.IsNullOrEmpty(createRoomName.text))
        {
            Debug.LogError("Enter Valid Room Name");
            return;
        }
        RoomOptions roomOptions = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 4
        };
        PhotonNetwork.JoinOrCreateRoom(createRoomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created by name: " + createRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creation Failed");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Joining Room Failed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(message: "room list updated : " + roomList.Count);
        foreach(var room in roomList)
        {
            if(room.RemovedFromList)
            {
                Debug.Log("removed from the list");
                for(int index = 0; index < listingList.Count; index++)
                {
                    if (listingList[index].roomInfo.Name == room.Name)
                    {
                        Destroy(listingList[index].gameObject);
                        listingList.RemoveAt(index);
                    }
                }
            }
            var listing = Instantiate(RoomListing, scrollViewContent);
            listingList.Add(listing);
            if(listing != null) listing.SetRoomInfo(room);
        }
    }
}
