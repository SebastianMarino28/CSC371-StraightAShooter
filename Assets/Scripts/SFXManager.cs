using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource bulletHit;

    public void playBulletHit()
    {
        bulletHit.Play();
    }
}
