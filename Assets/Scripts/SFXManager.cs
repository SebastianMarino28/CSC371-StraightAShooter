using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource bulletHit;
    public AudioSource doorLock;
    public AudioSource gameOver;
    public AudioSource aha1;

    public void playBulletHit()
    {
        bulletHit.Play();
    }

    public void playDoorLock()
    {
        doorLock.Play();
    }

    public void playGameOver()
    {
        gameOver.Play();
    }

    public void playAha1()
    {
        aha1.Play();
    }
}
