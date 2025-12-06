using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class KodamaFSM : AdvancedFiniteStateMachine
{
    [Header("Objects Essential")]
    public NavMeshAgent agent;
    public KodamaNetwork kodamaStats;

    protected float bulletRate = 0.3f;
    protected float bombRate = 0.7f;

    [Header("Arbitrary Timers and Counters")]
    protected float elapsedTimeBullet;
    protected float elapsedTimeBomb;
    protected float elapsedTimeEtc;

    [Header("AI Variables")]
    [Tooltip("How close to the target waypoint before moving to the next waypoint")]
    [SerializeField]
    private float waypointDistance;

    [Tooltip("Points that the tank will randomly move towards during patrol state")]
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private Transform currentTarget;

    [Tooltip("Find the scene's Game Manager")]
    [SerializeField]
    private GameObject arenaManager;
    private ArenaManager amObject;


    [Header("I just can't get the advanced fsm to work so here we are")]
    public bool patrolState;
    public bool fleeState;
    public bool defendState;
    public bool restState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patrolState = true;
        fleeState = false;
        defendState = false;
        restState = false;
        agent = GetComponent<NavMeshAgent>();
        arenaManager = GameObject.Find("GameManager");
        amObject = arenaManager.GetComponent<ArenaManager>();
        waypoints = amObject.wayPoints;
        RandomizeWaypointTarget();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeBullet += Time.deltaTime;
        elapsedTimeBomb += Time.deltaTime;
        elapsedTimeEtc += Time.deltaTime;
        
        if(kodamaStats.enemyHealth > 0)
        {
            StateRunner();
            StateChanger();
            CheckWayPointDistance();
        }
    }

    public void CheckWayPointDistance()
    {
        if(Vector3.Distance(kodamaStats.networkPosition, currentTarget.position) <= waypointDistance)
        {
            RandomizeWaypointTarget();
        }
    }

    public void StateChanger()
    {
        if(kodamaStats.enemyHealth <= (kodamaStats.enemyMaxHealth * 0.7) && patrolState == true)
        {
            patrolState = false;
            defendState = true;

            Debug.Log("Defending");
        }

        if(defendState == true && elapsedTimeEtc >= 30.0f)
        {
            defendState = false;
            fleeState = true;
            elapsedTimeEtc = 0.0f;

            Debug.Log("Fleeing");
        }

        if(fleeState == true && elapsedTimeEtc >= 30.0F)
        {
            fleeState = false;
            elapsedTimeEtc = 0.0f;
            restState = true;

            Debug.Log("Resting");
        }

        if (restState == true && (kodamaStats.enemyHealth >= (kodamaStats.enemyMaxHealth*0.8) || elapsedTimeEtc >= 10))
        {
            restState = false;
            elapsedTimeEtc = 0.0f;
            fleeState = true;

            Debug.Log("Fleeing");
        }
    }

    public void StateRunner()
    {
        if (patrolState == true)
        {
            Patrol();
        }
        else if (fleeState == true)
        {
            Flee();
        }
        else if (defendState == true)
        {
            Defend();
        }
        else if (restState == true)
        {
            Rest();
        }
        else
        {
            patrolState = true;
        }
    }

    public void Patrol()
    {
        agent.SetDestination(currentTarget.position);
    }

    public void Flee()
    {
        agent.SetDestination(currentTarget.position);
        elapsedTimeEtc += Time.deltaTime;
        elapsedTimeBomb += Time.deltaTime;
        if (elapsedTimeBomb >= bombRate)
        {
            kodamaStats.FireBombs();
            elapsedTimeBomb = 0.0f;
        }
    }

    public void Defend()
    {
        elapsedTimeEtc += Time.deltaTime;
        elapsedTimeBomb += Time.deltaTime;
        if(elapsedTimeBomb >= (bombRate/2) )
        {
            kodamaStats.FireBombs();
            elapsedTimeBomb = 0.0f;
        }

    }

    public void Rest()
    {
        elapsedTimeEtc += Time.deltaTime;
        kodamaStats.enemyHealth += 1;
    }

    public void RandomizeWaypointTarget()
    {
        // Randomize a value from the array.
        // Random.Range when int, max is exclusive so we can directly use the length of array
        int randomIndex = Random.Range(0, waypoints.Length);

        // Ensure that the next randomized waypoint is unique
        while (waypoints[randomIndex] == currentTarget)
        {
            randomIndex = Random.Range(0, waypoints.Length);
        }
        SetCurrentTarget(waypoints[randomIndex]);
    }

    private void SetCurrentTarget(Transform target)
    {
        currentTarget = target;
    }
}
