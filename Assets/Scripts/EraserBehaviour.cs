using UnityEngine;

public class EraserBehaviour : MonoBehaviour
{
    public GameObject eraserRing;
    public float scaleSpeed;
    public float maxSize;
    private Vector3 scale = Vector3.up;
    void Start()
    {
        transform.localScale = scale;
        GameObject ring = Instantiate(eraserRing);
        ring.transform.position = transform.position;
    }

    void Update()
    {
        if (scale.x >= maxSize)
        {
            Destroy(gameObject);
        }
        scale.x += Time.deltaTime * scaleSpeed;
        scale.z += Time.deltaTime * scaleSpeed;

        transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
