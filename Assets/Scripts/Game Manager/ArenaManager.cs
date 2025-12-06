using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class ArenaManager : MonoBehaviourPunCallbacks
{

    private int playerID;

    [Header("Prefabs")]
    public GameObject playerCat;
    public GameObject kodamaprefab;

    [Header("SpawnPoints")]
    public Transform[] spawnPoint;

    [Header("Level Loadings")]
    public string mainMenu;

    [Header("Enemy Waypoints")]
    public Transform[] wayPoints;

    [Header("Player Tracker")]
    public Transform[] playerPositions;


    private void Awake()
    {
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
        StartCoroutine(ArenaSpawn());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LoadLevel(mainMenu);
        }

    }

    public void WinCondition()
    {
        Debug.Log("You all won!");
        PhotonNetwork.LoadLevel(mainMenu);
    }

    IEnumerator ArenaSpawn()
    {
        Transform spawnLocation;

        spawnLocation = spawnPoint[5];

        yield return new WaitForSeconds(0.2f); //delay spawn by a few frames

        PhotonNetwork.Instantiate(kodamaprefab.name, spawnLocation.position, spawnLocation.rotation);

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Transform spawnLocation;

        // Prevent double-spawning if the player already exists
        

        //for some reason switch case wasnt working????

        if (playerID == 1)
        {
            spawnLocation = spawnPoint[0];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);

        }
        else if (playerID == 2)
        {
            spawnLocation = spawnPoint[1];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        }
        else if (playerID == 3)
        {
            spawnLocation = spawnPoint[2];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        }
        else if (playerID == 4)
        {
            spawnLocation = spawnPoint[3];
            Debug.Log("Spawning Player " + playerID + " at " + spawnLocation.name);
        }
        else
        {
            spawnLocation = spawnPoint[0];
            Debug.Log("Spawn Error, defaulting to player 1 spawn");
        }

        PhotonNetwork.Instantiate(playerCat.name, spawnLocation.position, spawnLocation.rotation);

    }
}
