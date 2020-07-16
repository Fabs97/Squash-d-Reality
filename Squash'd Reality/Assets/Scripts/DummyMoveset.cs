﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DummyMoveset : NetworkBehaviour
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

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        life = 100000;
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (hasAuthority)
        {
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
        Vector3 moveRightStick = new Vector3(Input.GetAxis("Horizontal-Direction"), 0, -Input.GetAxis("Vertical-Direction"));
        controller.Move(move * Time.deltaTime * playerSpeed*playerSpeedMultiplier);

        if(moveRightStick != Vector3.zero){
            gameObject.transform.forward = moveRightStick;

        }else if (moveRightStick == Vector3.zero & move != Vector3.zero){
            gameObject.transform.forward = move;
        }

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

    public void Die(string playerDead) {
        UIManager _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if (hasAuthority)
        {
            _uiManager.setInfoBoxText("YOU DIED"); 
        }
        else
        {
            _uiManager.setInfoBoxText(playerDead + " DIED");
        }
        _uiManager.setInfoBoxActive(true);

    }

    //Use this for trench time
    public void TakeDamage(int damage)
    {
        UIManager _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        //disable Spartan Armor
        if (life == 2 && hasAuthority)
        {
            _uiManager.setPowerUpButtonActive(false);
        }
        life = life - damage;
        if (life <= 0)
        {
            if (hasAuthority)
            {
                _uiManager.setInfoBoxText("YOU DIED");        
                _uiManager.setInfoBoxActive(true);   
            }
            GameObject.FindObjectOfType<TrenchTime>().setPlayerDead();
            Destroy(this.gameObject);
        }   
        
    }
    //-------------------------------------------POWER UPs SETTINGS---------------------------------------
    public void setSpartanArmorActive()
    {
        resetPowerUpValues();
        life = 2;
    }

    public void setHoverboardActive()
    {
        resetPowerUpValues();
        playerSpeedMultiplier = hoverboardMultiplier;
        durationPowerup = StartCoroutine(powerUpDuration());
    }

    public void setPogoStickActive()
    {
        resetPowerUpValues();
        pogoStickActive = true;
        jumpHeightMultiplier = pogoStickMultiplier;
        durationPowerup = StartCoroutine(powerUpDuration());
    }
    public void resetPowerUpValues()
    {
        if (durationPowerup != null)
        {
            StopCoroutine(durationPowerup);

        }
        life = 1;
        playerSpeedMultiplier = 1f;
        jumpHeightMultiplier = 1.3f;
        pogoStickActive = false;
    }

    IEnumerator powerUpDuration()
    {
        yield return new WaitForSeconds(10f);
        UIManager _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        resetPowerUpValues();
        if (hasAuthority)
        {
         _uiManager.setPowerUpButtonActive(false);   
        }
    }

}
