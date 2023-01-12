using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GestionJoueur : MonoBehaviourPunCallbacks, IPunObservable
{
    public float Health = 1f;
    public static GameObject LocalPlayerInstance;
    [SerializeField]
    private GameObject playerUiPrefab;
    [SerializeField]
    private GameObject beam;
    bool isShooting = false;
    //gestion health
    public int maxHealth = 100;
    public int currentHealth;
    //public HealthBBar healthBar;

    private void Awake()
    {
        
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
       
        DontDestroyOnLoad(gameObject);

      //  HealthBBar = GetComponent<HealthBBar>();
    }



    // Start is called before the first frame update
    void Start()
    {
        GestionCamera gestionCamera = gameObject.GetComponent<GestionCamera>();
        if (gestionCamera != null)
        {
            
            if (photonView.IsMine)
            {
                gestionCamera.OnStartFollowing();
            }
           
        }
        else
        {
            Debug.Log("Pas de script GestionCamera sur le player prefab");
        }
        if (this.playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

        //gestion health
        currentHealth = maxHealth;
    }
    public override void OnDisable() { 
    
    base.OnDisable();
    UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

// Update is called once per frame
void Update()
{
     if(photonView.IsMine)
    {
        if (Input.GetButton("Fire1"))
          
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!GestionAnimations.isShooting)
            {
                GestionAnimations.isShooting = true;
                this.beam.SetActive(true);
                photonView.RPC("JouerSon", RpcTarget.All);
            }
            
        }
            else
            {
                GestionAnimations.isShooting = false;
                this.beam.SetActive(false);
                //Activerbeam();
                photonView.RPC("StopSon", RpcTarget.All);
                ;            }
            if (this.Health <= 0)
            {
                QuitterRoom();
            }
        }

        


    }
    [PunRPC]
    void JouerSon()
    {
        GetComponent<AudioSource>().Play();
    }
    [PunRPC]
    void StopSon()
    {
        GetComponent<AudioSource>().Stop();
    }
    private void QuitterRoom()
{
    GestionJeu.Instance.Quitter();

}
    //Methodes des collisons
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        this.Health -= .1f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        this.Health -= .1f*Time.deltaTime;
    }

    //Au cas ou on charge des terrains de taille diffé
    void CalledOnLevelWasLoaded(int level)
    {
        if (Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }

        if (this.playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
}
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene,
        UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }

public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
{
        if (stream.IsWriting)   // envoi des data
        {
            stream.SendNext(this.isShooting);
            stream.SendNext(this.Health);
        }
        else //reception des data
        {
            this.isShooting = (bool)stream.ReceiveNext();
            this.Health=(float)stream.ReceiveNext();
        }

}

    //void bar de vie
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);
    }


}
