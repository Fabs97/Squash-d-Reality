using System;
using System.Collections;
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

    
    //ALLY DAMAMGE
    private float BasicDamage = 6.7f;
    private float MediumDamage = 13.4f;
    private float HighDamage = 20f;
    private float allyLife = 20f;
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
        PlayerStats playerStats = GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>();
        if (hasAuthority)
        {
            _uiManager.setInfoBoxText("YOU DIED");
            playerStats.death++;
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
                PlayerStats playerStats = GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>();
                playerStats.death++;
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
        GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().powerUp++;
        resetPowerUpValues();
        life = 2;
    }

    public void setHoverboardActive()
    {
        GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().powerUp++;
        resetPowerUpValues();
        playerSpeedMultiplier = hoverboardMultiplier;
        durationPowerup = StartCoroutine(powerUpDuration());
    }

    public void setPogoStickActive()
    {
        GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().powerUp++;
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
    //----------------------------------------------------TRIGGER BULLET------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && other.GetComponent<Bullet>().shooterName!=playerName)
        {
            if (other.gameObject.name == "BulletPistol")
            {
                allyLife -= BasicDamage;
            }else if (other.gameObject.name == "BulletShotgun")
            {
                allyLife -= BasicDamage;
            } else if (other.gameObject.name == "BulletAssaultRifle")
            {
                allyLife -= MediumDamage;
            }else if (other.gameObject.name == "BulletSniperRifle")
            {
                allyLife -= HighDamage;
            }else if (other.gameObject.name == "BulletSMG")
            {
                allyLife -= BasicDamage;
            }

            if (allyLife <= 0f)
            {
                TakeDamage(1);
                allyLife = 20f;
                if (life == 0)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    foreach (var player in players)
                    {
                        if (player.GetComponent<DummyMoveset>().playerName ==
                            other.gameObject.GetComponent<Bullet>().shooterName)
                        {
                            player.GetComponent<DummyMoveset>().friendlyKilled(); 
                        }
                    }                  

                }
            }
        }
        
    }

    //----------------------------------------------------SET OTHER STATS----------------------------------------------------------------------
    public void setCollectibleStats()
    {
        if (hasAuthority)
        {
            GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().collectible++;

        }
    }

    public void enemyKilled()
    {
        if (hasAuthority)
        {
            GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().antivirusKilled++;
        }
    }

    public void friendlyKilled()
    {
        if (hasAuthority)
        {
            GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().friendlyKill++;
        }
    }
    
}
