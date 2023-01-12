using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649
public class GestionJeu : MonoBehaviourPunCallbacks
{
    static public GestionJeu Instance;
    private GameObject instance;
    [SerializeField]
    private GameObject playerPrefab;
        [SerializeField]
    private Transform[] posInstances;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance!=null&&Instance!=this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Scene_connexion");
            return;
        }
        if (playerPrefab == null)
        {
            Debug.Log("Pas de reference pour le playerPrefab des GestionJeu");
        }
        else
        {
         
                if(GestionJoueur.LocalPlayerInstance == null)
               {
                    int rand=Random.Range(0, posInstances.Length);
                    PhotonNetwork.Instantiate(this.playerPrefab.name, posInstances[rand].transform.position, Quaternion.identity,0);
               }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
        
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Scene_connexion");
    }
    public void Quitter()
    {
        PhotonNetwork.LeaveRoom(); //PhotonNetwork
    }
   public void QuitApplication()
    {
        QuitApplication();
    }
    void LoadArena()
    {
        PhotonNetwork.LoadLevel("SceneJeu");
    }
}
