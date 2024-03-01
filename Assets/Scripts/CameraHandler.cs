using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Vector3 centerPoint;
    private GameObject player;
    public float horizLimit;
    public float vertLimit;
    public float horizOffset;
    public float vertOffset;
    public float cameraHeight;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        centerPoint = transform.position;
    }


    void Update()
    {
        if (player.transform.position.x > centerPoint.x + horizLimit)
        {
            // trigger transition animation and move camera

            centerPoint = new Vector3(centerPoint.x + horizOffset, centerPoint.y, centerPoint.z);
            transform.position = centerPoint;
        }
        else if (player.transform.position.x < centerPoint.x - horizLimit)
        {
            // trigger transition animation and move camera

            centerPoint = new Vector3(centerPoint.x - horizOffset, centerPoint.y, centerPoint.z);
            transform.position = centerPoint;
        }
        else if (player.transform.position.z > centerPoint.z + 7 + vertLimit)
        {
            // trigger transition animation and move camera

            centerPoint = new Vector3(centerPoint.x, centerPoint.y, centerPoint.z + vertOffset);
            transform.position = centerPoint;
        }
        else if (player.transform.position.z < centerPoint.z + 7 - vertLimit)
        {
            // trigger transition animation and move camera

            centerPoint = new Vector3(centerPoint.x, centerPoint.y, centerPoint.z - vertOffset);
            transform.position = centerPoint;
        }

    }
}
