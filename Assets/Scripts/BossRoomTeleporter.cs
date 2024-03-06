using UnityEngine;

public class BossRoomTeleporter : MonoBehaviour
{
    public Vector3 bossPoint;
    public Vector3 playerPoint;
    public Vector3 cameraPoint;
    public GameObject cam;
    public GameObject boss;
    public GameObject minimap;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = playerPoint;
            cam.GetComponent<CameraHandler>().enabled = false;
            cam.transform.position = cameraPoint;
            GameObject newBoss = Instantiate(boss);
            newBoss.transform.position = bossPoint;
            minimap.SetActive(false);
        }
    }
}
