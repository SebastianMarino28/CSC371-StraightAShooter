using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private float playerX;
    private float playerZ;
    private Vector3 camPos;
    private float camOffsetZ;

    void Start()
    {
        camOffsetZ = transform.position.z;
    }

    void Update()
    {
        // get player X and Z values, then make a new vector using the camera's Y
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        camPos = new Vector3(playerX, transform.position.y, playerZ + camOffsetZ);

        // assign cam position to the new position above the player
        transform.position = camPos;
    }
}
