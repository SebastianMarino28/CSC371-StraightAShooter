using UnityEngine;

public class RoofVisibility : MonoBehaviour
{
    private MeshRenderer visibility;
    public GameObject roomLight;

    void Start()
    {
        visibility = GetComponent<MeshRenderer>();
        roomLight.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            visibility.enabled = false;
            int mapX = (int) (((transform.position.x + 10) / 20));
            int mapY = (int) (((transform.position.z + 10) / 20));
            GameManager.instance.mapX = mapX;
            GameManager.instance.mapY = mapY;
            roomLight.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            visibility.enabled = true;
            roomLight.SetActive(false);
        }
    }
}
