using UnityEngine;

public class Destroyer : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndWall"))  {
            Destroy(other.gameObject);
        }
    }
}
