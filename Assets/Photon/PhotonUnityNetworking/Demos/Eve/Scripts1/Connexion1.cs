using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

#pragma warning disable 649
public class Connexion1 : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private Text feedbackText; // Text à changer
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    bool isConnecting;
    string gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Connect()
    {
        feedbackText.text = "";
        isConnecting = true;
        controlPanel.SetActive(false);
        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            LogFeedback("Connexion en cours...");
                PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    void LogFeedback(string message)
    {
        if(feedbackText != null)
        {
            feedbackText.text += System.Environment.NewLine+ message;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = this.maxPlayersPerRoom});
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        feedbackText.text = "";
        LogFeedback("D/connexion en cours..." + cause);
        isConnecting = false;
        controlPanel.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
          PhotonNetwork.LoadLevel("SceneJeu");
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
