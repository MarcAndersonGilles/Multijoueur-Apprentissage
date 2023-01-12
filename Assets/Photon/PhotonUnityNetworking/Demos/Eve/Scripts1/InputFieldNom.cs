using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


[RequireComponent(typeof(InputField))]
public class InputFieldNom : MonoBehaviour
{
    const string playerNomPrefKey = "PlayerNom";
    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNomPrefKey)){
                defaultName = PlayerPrefs.GetString(playerNomPrefKey);
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }
   public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Nom du joueur non valide");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNomPrefKey, value);
    }


}
