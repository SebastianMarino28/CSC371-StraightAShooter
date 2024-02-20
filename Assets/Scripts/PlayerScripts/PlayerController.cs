using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    public WeaponHandler wh;

    [Header("Movement Attributes")]
    public int speed;
    public float rollDistance;
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
    private float rollSpeed;
    private float rollStartTime;
    private float rollCooldownRemaining = 0;
    private Vector3 rollDirection;
    
    // health vars
    private UnityEngine.UI.Image healthBar;
    public float curHealth;
    public float maxHealth;
    private float baseProjectileDamage;


    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        animator = GetComponent<Animator>();
        rollSpeed = rollDistance / rollTime;
        curHealth = 50f;
        maxHealth = 50f;
        //healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<UnityEngine.UI.Image>();
        baseProjectileDamage = 5f;
    }

    void Update()
    {
        mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            Vector3 playerized = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
            transform.forward = (playerized - transform.position).normalized;
        }

        if (isFiring)
        {
            wh.FireWeapon(WeaponHandler.ProjectileType.bullet);
        }

        rollCooldownRemaining -= Time.deltaTime;
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
                rb.velocity = rollDirection * rollSpeed * Time.fixedDeltaTime;
            }
            // roll complete
            else
            {
                animator.SetBool("isRolling", false);
                isRolling = false;
                rb.velocity = Vector3.zero;

                //start roll cooldown timer
                rollCooldownRemaining = rollCooldown;
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
        if (Time.timeScale > 0 && fireValue.isPressed && !isRolling)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Projectile")) {
            Destroy(other.gameObject);
            TakeDamage(baseProjectileDamage);
            
        }
    }

    void OnRoll(InputValue rollValue)
    {
        if (Time.timeScale > 0 && !isRolling && rollCooldownRemaining  <= 0)
        {
            isRolling = true;
            animator.SetBool("isRolling", true);

            rollStartTime = Time.time;
            //roll in moving direction if moving, or in facing direction (towards cursor) if stationary
            if (isMoving)
            {
                rollDirection = new Vector3(movementX, 0, movementZ).normalized;
            }
            else
            {
                rollDirection = transform.forward;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(curHealth > 0) {
            curHealth -= damage;
            healthBar.fillAmount = curHealth / maxHealth;
        }
        else {      // game over here
            Debug.Log("You Lose!");
        }
    }
}
