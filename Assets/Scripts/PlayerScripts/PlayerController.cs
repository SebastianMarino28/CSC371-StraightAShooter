using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;


public class PlayerController : MonoBehaviour
{
    public GameObject hit;
    public Camera mainCamera;
    public LayerMask layerMask;
    public WeaponHandler wh;
    private Animator anim;
    private SFXManager sfxManager;
    //public InputAction pauseAction;            // set to the Pause input (ESC/P key)

    // UI
    public PauseMenuBehaviour pauseMenu;
    public UIButtonBehaviour ui_buttons;


    [Header("Roll Attributes")]
    public float rollMultiplier;
    public float rollInvincibilityDelay;
    public float rollInvinicilityDuration;
    public float rollCooldown;


    //movement vars
    private Vector3 mousePosition;
    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private bool isFiring;


    // animation state
    private Animator animator;
    private bool isMoving = false;
    private bool isRolling = false;


    // roll vars
    private const float rollTime = 0.6f; // this number comes from the animation time (1.2s) conducted at double speed (1.2s / 2 = 0.6s)
    private float rollStartTime;
   
    // health vars
    public UnityEngine.UI.Image healthBar;
    private float baseProjectileDamage;
    private float baseLaserDamage;
    private float baseMeleeDamage;
    private bool isInvincible;

    [Header("Stats")]
    public int speed;
    public float damage;
    public float defense;
    public float curHealth;
    public float maxHealth;
    public int speedLevel;
    public int damageLevel;
    public int defenseLevel;

    // powerup vars
    public enum PowerupType
    {
        roll,
        shield,
        eraser
    }

    private List<PowerupType> powerups = new List<PowerupType>();
    private int powerupIndex = 0;
    private bool canUsePowerupRoll = true;
    private bool canUsePowerupShield = true;
    private bool canUsePowerupEraser = true;

    public GameObject dodgeIcon;
    public GameObject shieldIcon;
    public GameObject eraserIcon;
    public UnityEngine.UI.Image abilityChargeRoll;
    public UnityEngine.UI.Image abilityChargeShield;
    public UnityEngine.UI.Image abilityChargeEraser;

    // shield vars
    public GameObject shield;  
    public float shieldCooldown;

    // eraser vars
    public GameObject eraser;
    public float eraserCooldown;

    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  
        curHealth = maxHealth;
        powerups.Add(PowerupType.roll);
        baseProjectileDamage = 5f;
        baseMeleeDamage = 6f;
        baseLaserDamage = 5f;
        animator = GetComponent<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        abilityChargeRoll.enabled = true;
        abilityChargeShield.enabled = false;
        abilityChargeEraser.enabled = false;
    }


    void Update()
    {
        mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask) && Time.timeScale > 0.0000001f)
        {
            Vector3 playerized = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
            transform.forward = (playerized - transform.position).normalized;
        }


        if (isFiring && !isRolling)
        {
            wh.FireWeapon(WeaponHandler.ProjectileType.bullet);
        }
    }


     void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);


        if(!isRolling)
        {
            rb.velocity = speed * Time.fixedDeltaTime * movement;
        }


        if(isRolling)
        {
            float elapsedTime = Time.time - rollStartTime;


            if(elapsedTime < rollTime)
            {
                rb.velocity = (speed * rollMultiplier) * Time.fixedDeltaTime * movement;
            }
            // roll complete
            else
            {
                animator.SetBool("isRolling", false);
                isRolling = false;
                StartCoroutine(Cooldown(rollCooldown, "roll", abilityChargeRoll));
            }          
        }
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();


        movementX = movementVector.x;
        movementZ = movementVector.y;


        //set movement state for animation
        if (movementVector.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
        }
    }


    void OnFire(InputValue fireValue)
    {
        if (Time.timeScale > 0.0000001f && fireValue.isPressed)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        } 
    }

    void OnPause(InputValue pauseValue)
    {
        pauseMenu.Pause(pauseValue);
    }

    void OnBackpack(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if(!pauseMenu.paused)
                ui_buttons.BackpackClick();
            else
                if(pauseMenu.menuStack.Count == 1 && pauseMenu.menuStack.Peek().name == "PauseScreen") {
                    pauseMenu.PausePushPop(null);
                    ui_buttons.BackpackClick();
                }
                else 
                    pauseMenu.PausePushPop(null);

        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy") && !isRolling && !isInvincible)
        {
            TakeDamage(baseMeleeDamage);
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Projectile") && !isRolling && !isInvincible) {
            Destroy(other.gameObject);
            TakeDamage(baseProjectileDamage);
           
        }
        if (other.gameObject.CompareTag("Laser") && !isRolling && !isInvincible) {
            TakeDamage(baseLaserDamage);
           
        }
        if (other.gameObject.CompareTag("TPBubbleTrigger")) {
           GameObject tp_final = GameObject.FindGameObjectWithTag("TPFinal");
           tp_final.GetComponent<Canvas>().enabled = true;
           tp_final.GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("TPBubbleTrigger")) {
           GameObject tp_final = GameObject.FindGameObjectWithTag("TPFinal");
           tp_final.GetComponent<Canvas>().enabled = false;
           tp_final.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    void OnAbility(InputValue rollValue)
    {
        if (powerups[powerupIndex] == PowerupType.roll && canUsePowerupRoll)
        {
            Roll();
        }
        if (powerups[powerupIndex] == PowerupType.shield && canUsePowerupShield)
        {
            Shield();
        }
        if (powerups[powerupIndex] == PowerupType.eraser && canUsePowerupEraser)
        {
            Eraser();
        }

    }

    void Roll()
    {
        if (Time.timeScale > 0.0000001f && !isRolling)
        {
            isRolling = true;
            animator.SetBool("isRolling", true);

            rollStartTime = Time.time;
            canUsePowerupRoll = false;
        }
    }

    void Shield()
    {
        GameObject newShield = Instantiate(shield);
        newShield.transform.position = transform.position;
        StartCoroutine(Cooldown(shieldCooldown, "shield", abilityChargeShield));
    }

    void Eraser()
    {
        GameObject newEraser = Instantiate(eraser);
        newEraser.transform.position = transform.position;
        StartCoroutine(Cooldown(eraserCooldown, "eraser", abilityChargeEraser));
    }

    void OnSwitch()
    {
        if (powerups.Count > 1)
        {
            powerupIndex++;
            if (powerupIndex == powerups.Count)
            {
                powerupIndex = 0;
            }
        }
        dodgeIcon.SetActive(false);
        shieldIcon.SetActive(false);
        eraserIcon.SetActive(false);
        abilityChargeRoll.enabled = false;
        abilityChargeShield.enabled = false;
        abilityChargeEraser.enabled = false;
        if (powerups[powerupIndex] == PowerupType.roll)
        {
            dodgeIcon.SetActive(true);
            abilityChargeRoll.enabled = true;
        }
        if (powerups[powerupIndex] == PowerupType.shield)
        {
            shieldIcon.SetActive(true);
            abilityChargeShield.enabled = true;
        }
        if (powerups[powerupIndex] == PowerupType.eraser)
        {
            eraserIcon.SetActive(true);
            abilityChargeEraser.enabled = true;
        }
        Debug.Log(powerups[powerupIndex]);
    }


    public void TakeDamage(float damage)
    {
        if(curHealth > 0) {
            sfxManager.playPain();
            GameObject effect = Instantiate(hit);
            effect.transform.position = transform.position;
            curHealth -= damage - (damage * defense); // 10 defense will result in a 60% damage reduction 
            healthBar.fillAmount = curHealth / maxHealth;
            StartCoroutine(Invincibility());
        }
        if(curHealth <= 0) {
            Time.timeScale = 0f;
            //pauseAction.action.Disable();
            GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<Animator>().Play("GameOverFadeIn");
            Debug.Log("You Lose!");
            sfxManager.playGameOver();
        }
    }

    public void UnlockShield()
    {
        powerups.Add(PowerupType.shield);

        GameObject statScreen = GameObject.FindGameObjectWithTag("StatsScreen");
        GameObject shieldInfoPrefab = (GameObject)Resources.Load("Prefabs/AbilityInfo/Shield_Info");
        Destroy(GameObject.Find("LockedShieldInfo"));
        Instantiate(shieldInfoPrefab, statScreen.transform);
    }
    public void UnlockEraser()
    {
        powerups.Add(PowerupType.eraser);

        GameObject statScreen = GameObject.FindGameObjectWithTag("StatsScreen");
        GameObject eraserInfoPrefab = (GameObject)Resources.Load("Prefabs/AbilityInfo/Eraser_Info");
        Destroy(GameObject.Find("LockedEraserInfo"));
        Instantiate(eraserInfoPrefab, statScreen.transform);
    }


    IEnumerator Cooldown(float cooldowntime, string type, UnityEngine.UI.Image abilityCharge)
    {
        switch(type)
        {
            case "roll":
                canUsePowerupRoll = false;
                break;
            case "shield":
                canUsePowerupShield = false;
                break;
            case "eraser":
                canUsePowerupEraser = false;
                break;
            default:
                break;
        }

        float totalTime = 0.0f; // Total time elapsed
        float duration = cooldowntime; // Total duration of the cooldown

        while (totalTime < duration)
        {
            float progress = totalTime / duration; // Calculate progress based on elapsed time
            abilityCharge.fillAmount = progress; // Update the fill amount

            totalTime += Time.deltaTime; // Increment total time based on frame time
            yield return null; // Wait for the next frame
        }

        // Ensure the fill amount is exactly 1.0 at the end
        abilityCharge.fillAmount = 1.0f;

        switch(type)
        {
            case "roll":
                canUsePowerupRoll = true;
                break;
            case "shield":
                canUsePowerupShield = true;
                break;
            case "eraser":
                canUsePowerupEraser = true;
                break;
            default:
                break;
        }
    }
    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}
