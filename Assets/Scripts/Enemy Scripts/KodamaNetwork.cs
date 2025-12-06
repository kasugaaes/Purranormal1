using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.VisualScripting;

public class KodamaNetwork : MonoBehaviourPunCallbacks
{
    [Header("HealthBar")]
    public Slider enemyHealthBar;
    public float enemyMaxHealth;
    public float enemyHealth;

    [Header("Weapon")]
    public Transform[] bombSpawnPoint;
    public GameObject bomb;
    public Transform bulletSpawnPoint;
    public GameObject bullet;


    [Header("Getting the Game Manager")]
    public GameObject arenaManager;
    public ArenaManager arena;


    [Header("Network Sync")]
    public Vector3 networkPosition;
    public Quaternion networkRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        enemyHealthBar.value = (enemyMaxHealth / enemyHealth);
        arenaManager = GameObject.Find("GameManager");
        arena = arenaManager.GetComponent<ArenaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient == false)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float damage;


        if (collision.gameObject.CompareTag("PlayerAttack") && PhotonNetwork.IsMasterClient)
        {
            damage = Random.Range(5.0f, 25.0f);
            photonView.RPC("Damage", RpcTarget.All, damage);
        }
    }

    [PunRPC]
    public void Damage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy Hit");
        enemyHealthBar.value = (enemyHealth / enemyMaxHealth);

        if (enemyHealth <= 0)
        {
            arena.WinCondition();
        }
    }

    [PunRPC]
    public void FireBombs()
    {
        for(int i = 0; i<=3 ; i++)
        {
            PhotonNetwork.Instantiate(bomb.name, bombSpawnPoint[i].position, bombSpawnPoint[i].rotation);
        }
    }
}
