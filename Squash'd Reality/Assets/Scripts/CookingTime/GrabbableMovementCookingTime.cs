using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabbableMovementCookingTime :  NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private static float initSpeed= 3.0f;
    private float playerSpeed = initSpeed;
    private float playerSpeedMultiplier = 1f;
    [SerializeField] private float jumpHeight = 0.5f;
    private float jumpHeightMultiplier = 1f;
    private float gravityValue = -9.81f;
  


    public bool cubeMovement = false;
    
  
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cubeMovement = false;
        controller.enabled = false;
    }
    
    void FixedUpdate()
    {
        if (hasAuthority && cubeMovement)
        {
            Move();
        }

        if (transform.position.y<=0.56f)
        {
            controller.enabled = true;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    
    void Move()
    {
        Debug.Log("PLAYER VELOCITY:" + playerVelocity);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed*playerSpeedMultiplier);
        
        // Changes the height position of the cube..
        if (Input.GetButton("Jump") && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * jumpHeightMultiplier * -4.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
}
