using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private int playerID;

    [Header("Prefabs")]
    public GameObject playerCat;

    [Header("SpawnPoints")]
    public Transform[] spawnPoint;

    [Header("Level Loadings")]
    public string mainMenu;
    public string arenaToLoad;

    //
    //
    //Decided to include spawner in the lobby manager
    //
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        arenaToLoad = null;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("This is the Master Client");
        }
        Cursor.lockState = CursorLockMode.None;

        // Optionally, hide the cursor (typical for FPS games)
        Cursor.visible = true;


        playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        StartCoroutine(LobbySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
                PhotonNetwork.LoadLevel(mainMenu);
        }

    }

    public void LoadArena(string arenaToLoad)
    {
        PhotonNetwork.LoadLevel(arenaToLoad);
    }

    //Joiners
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room, spawning player...");
        StartCoroutine(LobbySpawn());
    }

    IEnumerator LobbySpawn()
    {
        yield return new WaitForSeconds (0.2f); //delay spawn by a few frames
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Transform spawnLocation;

        // Prevent double-spawning if the player already exists
        if (PhotonNetwork.LocalPlayer.TagObject != null)
        {
            Debug.Log("Player already spawned, skipping.");
            return;
        }

        //for some reason switch case wasnt working????

        if (playerID == 1)
        {
            spawnLocation = spawnPoint[0];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        } else if(playerID == 2)
        {
            spawnLocation = spawnPoint[1];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        } else if(playerID == 3)
        {
            spawnLocation = spawnPoint[2];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        } else if (playerID == 4)
        {
            spawnLocation = spawnPoint[3];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        } else
        {
            spawnLocation = spawnPoint[0];
            Debug.Log("Spawn Error, defaulting to player 1 spawn");
        }

            GameObject newPlayer = PhotonNetwork.Instantiate(playerCat.name, spawnLocation.position, spawnLocation.rotation);
            PhotonNetwork.LocalPlayer.TagObject = newPlayer;

    }


}
