using System.Collections;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public float ttl;

    void Start()
    {
        StartCoroutine(KillAfterSeconds(ttl));
    }

    IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
