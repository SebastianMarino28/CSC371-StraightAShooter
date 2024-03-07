using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;

    Vector3 spin = new Vector3(0, 1, 0);
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime * spin);
    }

}

