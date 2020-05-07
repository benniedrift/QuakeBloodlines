using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//all code found on photon pun google page found here : 

public class Launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");

        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        StartGame();

        base.OnJoinedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Create();

        base.OnJoinRandomFailed(returnCode, message);
    }

    public void Connect()
    {
        Debug.Log("Trying To Connect");

        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void Create()
    {
        PhotonNetwork.CreateRoom("");
    }
    
    public void StartGame()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
