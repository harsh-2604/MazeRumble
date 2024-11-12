using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class roomListing : MonoBehaviour
{
    public TMP_Text _roomListingText;
    public RoomInfo roomInfo;

    private void Awake()
    {
        _roomListingText = GetComponentInChildren<TMP_Text>();
    }

    public void SetRoomInfo(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        _roomListingText.text = _roomInfo.MaxPlayers + "|" + _roomInfo.Name; 
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }
}
