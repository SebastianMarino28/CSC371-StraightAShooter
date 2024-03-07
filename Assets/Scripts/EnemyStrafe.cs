using System.Collections;
using UnityEngine;

public class EnemyStrafe : MonoBehaviour
{
    private bool horizontal = false;
    private bool vertical = false;
    private Rigidbody rb;
    public float moveSpeed;
    public float strafeTime;
    private Vector3 destination;
    private Vector3 horizVector = new(1, 0, 0);
    private Vector3 vertVector = new(0, 0, 1);

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (Random.Range(0,2) == 0)
        {
            horizontal = true;
        }
        else
        {
            vertical = true;
        }

        StartCoroutine(StrafeTimer());
    }

    void FixedUpdate()
    {
        if (horizontal)
        {
            destination = transform.position + horizVector;
        }
        if (vertical)
        {
            destination = transform.position + vertVector;
        }

        Vector3 movement = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movement);
    }

    IEnumerator StrafeTimer()
    {
        yield return new WaitForSeconds(strafeTime);
        horizVector = -horizVector;
        vertVector = -vertVector;
        StartCoroutine(StrafeTimer());
    }
}
