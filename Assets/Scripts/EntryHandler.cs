using UnityEngine;

public class EntryHandler : MonoBehaviour
{
    private RoomHandler roomHandler;

    void Start()
    {
        roomHandler = transform.parent.GetComponent<RoomHandler>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomHandler.Enter();
        }
    }
}
