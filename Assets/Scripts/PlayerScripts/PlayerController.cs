using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    public WeaponHandler wh;

    public int speed;
    private Vector3 mousePosition;
    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private bool isFiring;

    // animation state
    private Animator animator;
    private bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
    }

     void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
        rb.velocity = speed * Time.fixedDeltaTime * movement;

        //set movement state for animation
        if (movement.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnFire(InputValue fireValue)
    {
        if (Time.timeScale > 0 && fireValue.isPressed)
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
            // deal damage to player
            
        }
    }
}
