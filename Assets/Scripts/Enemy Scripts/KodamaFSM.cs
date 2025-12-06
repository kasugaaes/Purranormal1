using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class KodamaFSM : AdvancedFiniteStateMachine
{
    [Header("Objects Essential")]
    public NavMeshAgent agent;
    public KodamaNetwork kodamaStats;

    [Header("Weapon")]
    public Transform bombSpawnPoint;
    public Transform bulletSpawnPoint;

    protected float shootRate = 0.3f;
    protected float elapsedTime;

    [Header("AI Variables")]
    [Tooltip("How close to the player is considered as a chasing range")]
    [SerializeField]
    private float chaseDistance;

    [Tooltip("How close to the player is considered as a Attack range")]
    [SerializeField]
    private float attackDistance;

    [Tooltip("How close to the target waypoint before moving to the next waypoint")]
    [SerializeField]
    private float waypointDistance;

    [Tooltip("Points that the tank will randomly move towards during patrol state")]
    [SerializeField]
    private Transform[] waypoints;

    [Tooltip("Reference to the player tank")]
    [SerializeField]
    private Transform player;

    [Tooltip("Find the scene's Game Manager")]
    [SerializeField]
    private GameObject arenaManager;
    private ArenaManager amObject;



    protected override void Initialize()
    {
        base.Initialize();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arenaManager = GameObject.Find("GameManager");
        amObject = arenaManager.GetComponent<ArenaManager>();
        waypoints = amObject.wayPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
