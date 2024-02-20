using UnityEngine;

public class RoofVisibility : MonoBehaviour
{
    private MeshRenderer visibility;

    void Start()
    {
        visibility = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            visibility.enabled = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            visibility.enabled = true;
        }
    }
}
