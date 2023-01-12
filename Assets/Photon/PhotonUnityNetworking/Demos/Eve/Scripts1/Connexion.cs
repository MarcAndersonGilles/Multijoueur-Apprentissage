using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Connexion : MonoBehaviour
{
    string version = "1";
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = version;
    }
}
