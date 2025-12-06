using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Collections;

public class PlayerCharacter : MonoBehaviourPunCallbacks
{
    [Header("Attack Objects and Essentials")]
    public GameObject attackHurtBox;
    public float attackTimer;
    public float attackSpeed;
    public bool canMove;

    float speed = 5f;
    Vector3 move = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    [Header("Network Sync")]
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    private float curSpeed, targetSpeed, rotSpeed;

    CharacterController characterController;


    public void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Optionally, hide the cursor (typical for FPS games)
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(canMove == true)
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer >= attackSpeed)
                {
                    photonView.RPC("Attack", RpcTarget.All);
                }

                UpdateMovement();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10f);
                transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10f);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Optionally, hide the cursor (typical for FPS games)
            Cursor.visible = false; 
        }

        attackTimer += Time.deltaTime;
        
    }

    private void UpdateMovement()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * 3f, 0);
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        move = Vector3.ClampMagnitude(move, 1f);
        velocity = transform.TransformVector(move) * speed;
        characterController.SimpleMove(velocity);

    }

    [PunRPC]
    IEnumerator Attack()
    {
        attackTimer = 0.0f;
        attackHurtBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHurtBox.SetActive(false);
    }
}
