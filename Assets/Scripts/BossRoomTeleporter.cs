using UnityEngine;

public class BossRoomTeleporter : MonoBehaviour
{
    public Vector3 bossPoint;
    public Vector3 playerPoint;
    public Vector3 cameraPoint;
    public GameObject player;
    public GameObject cam;
    public GameObject boss;
    public GameObject minimap;
    public BGMusicSelector bgMusic;

    public void Teleport()
    {
        bgMusic.StartBossMusic();
        player.transform.position = playerPoint;
        player.GetComponent<PlayerController>().inCombat = true;
        cam.GetComponent<CameraHandler>().enabled = false;
        cam.transform.position = cameraPoint;
        GameObject newBoss = Instantiate(boss);
        newBoss.transform.position = bossPoint;
        minimap.SetActive(false);
    }
}
