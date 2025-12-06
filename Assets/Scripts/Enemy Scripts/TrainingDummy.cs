using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


public class TrainingDummy : MonoBehaviourPunCallbacks
{
    [Header("HealthBar")]
    public Slider enemyHealthBar;
    public float enemyMaxHealth;
    public float enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        enemyHealthBar.value = (enemyMaxHealth/enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        float damage;


        if(collision.gameObject.CompareTag("PlayerAttack") && PhotonNetwork.IsMasterClient)
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

        if(enemyHealth <= 0)
        {
            enemyHealth = enemyMaxHealth;
        }
    }

}
