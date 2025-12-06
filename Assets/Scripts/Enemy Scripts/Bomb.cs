using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Bomb : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float lifeTime = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(lifeTime);
        PhotonNetwork.Destroy(gameObject);
    }
}
