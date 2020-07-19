using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeMovement :  NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private static float initSpeed= 3.0f;
    private float playerSpeed = initSpeed;
    private float playerSpeedMultiplier = 1f;
    private float hoverboardMultiplier = 1.3f;
    [SerializeField] private float jumpHeight = 0.5f;
    private float jumpHeightMultiplier = 1f;
    private float pogoStickMultiplier = 1.3f;
    private float gravityValue = -9.81f;

    [SyncVar] public string playerName;
    public int life;

    private Coroutine durationPowerup;

    private bool pogoStickActive;


    public bool cubeMovement = false;
    
    //ALLY DAMAMGE
    private float BasicDamage = 6.7f;
    private float MediumDamage = 13.4f;
    private float HighDamage = 20f;
    private float allyLife = 20f;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        life = 100000;
        cubeMovement = false;
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (hasAuthority && cubeMovement)
        {
            Debug.Log("PLAYER NAME: " + playerName);
            Move();
        }
    }

    void Move(){
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if(Input.GetButtonDown("Sprint")){
            playerSpeed = initSpeed*2;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed*playerSpeedMultiplier);

      

        // Changes the height position of the player..
        if (Input.GetButton("Jump") && groundedPlayer && !pogoStickActive) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * jumpHeightMultiplier * -4.0f * gravityValue);
        }

        if (pogoStickActive && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * jumpHeightMultiplier * -4.0f * gravityValue);

        }

        if (!groundedPlayer)
        {
            controller.slopeLimit = 90f;
        }
        else
        {
            controller.slopeLimit = 45f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
    
    
}
