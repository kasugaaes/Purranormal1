using Photon.Pun;
using UnityEngine;

public class KodamaArenaTrigger : MonoBehaviourPunCallbacks
{
    [Header("Object Essentials")]
    public LobbyManager lobbyManager;
    public string enemyKeyName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //just define the enemy in the inspector
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                lobbyManager.LoadArena(enemyKeyName);
                Debug.Log("Attempting to load Arena");
            }

        }
    }
}
