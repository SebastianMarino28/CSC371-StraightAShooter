using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float speed;

    Vector3 spin = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        spin = speed * Time.deltaTime * spin;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spin);
    }

    void OnInteract() {
        spin = -spin;
    }
}

