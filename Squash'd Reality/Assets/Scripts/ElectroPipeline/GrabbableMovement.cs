using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GrabbableMovement :  NetworkBehaviour
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
    
    private Scene scene;
    private bool darkPuzzle = false;


    public string grabbedBy;

    [SyncVar] public bool cubeMovement = false;
    
    [SerializeField] private float snapValue = 1.0f;

    private bool canMove = false;
    
    
    [SerializeField] private GameObject moveBox;
    
    private void Start()
    
    {
        grabbedBy = "default";
        controller = gameObject.GetComponent<CharacterController>();
        if (isServer)
        {
            cubeMovement = false;
 
        }
        
        scene = SceneManager.GetActiveScene();
        if (scene.name == "DarkPuzzle")
        {
            darkPuzzle = true;
        }

        if (scene.name == "ElectroPipeline")
        {
            if (moveBox != null)
            {
                moveBox.SetActive(false);
    
            }
            jumpHeightMultiplier = 0f;
            controller.slopeLimit = 0f;
            controller.enabled = false;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<NetworkTransform>().enabled = false;
        }

    }
    
    void FixedUpdate()
    {
        if (!darkPuzzle)
        {
            if (hasAuthority && cubeMovement && !canMove)
            {
                StartCoroutine(wait2());
                GetComponent<NetworkTransform>().enabled = true;
                controller.enabled = true;
            }else if (hasAuthority && cubeMovement && canMove)
            {
                Move();

            } else if (!cubeMovement && !darkPuzzle)
            {
                StartCoroutine(wait3());
            }else if (!cubeMovement && darkPuzzle)
            {
                Fall();
            }

            if (cubeMovement)
            {
                GetComponent<NetworkTransform>().enabled = true;
                controller.enabled = true;
                StartCoroutine(wait2());
            }
            else
            {
                StartCoroutine(wait());
            }   
        }
        else
        {
            if (hasAuthority && cubeMovement)
            {
                Move();
            }else if (!cubeMovement && !darkPuzzle)
            {
                float x = Mathf.Round(gameObject.transform.position.x / snapValue);
                float y = 0.55f;
                float z = Mathf.Round(gameObject.transform.position.z / snapValue);
                this.transform.position = new Vector3(x,y,z);
            }else if (!cubeMovement && darkPuzzle)
            {
                Fall();
            }
        }
        

    }

    IEnumerator wait3()
    {
        if (moveBox != null)
        {
            moveBox.SetActive(false);
    
        }
        yield return new WaitForSeconds(1f);
        if (!cubeMovement)
        {
            controller.enabled = false;
            float x = Mathf.Round(gameObject.transform.position.x / snapValue);
            float y = 0.55f;
            float z = Mathf.Round(gameObject.transform.position.z / snapValue);
            this.transform.position = new Vector3(x,y,z);
        }
        
    }
    
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
        if (cubeMovement && moveBox!=null)
        {
            moveBox.SetActive(true);
        }
    }
    
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        canMove = false;
        GetComponent<NetworkTransform>().enabled = false;
    }

    void Move(){
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

     void Fall()
     {
         groundedPlayer = controller.isGrounded;
         if (groundedPlayer && playerVelocity.y < 0)
         {
             playerVelocity.y = 0f;
         }
         playerVelocity.y += gravityValue * Time.deltaTime;
         controller.Move(playerVelocity * Time.deltaTime);
    }
     
    
}
