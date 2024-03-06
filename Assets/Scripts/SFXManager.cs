using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource bulletHit;
    public AudioSource doorLock;
    public AudioSource gameOver;
    public AudioSource aha1;
    public AudioSource pain1;
    public AudioSource pain2;
    public AudioSource drink;
    public AudioSource deepBreath;
    public AudioSource scribble;
    public AudioSource munch;

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

    public void playAha()
    {
        aha1.Play();
    }

    public void playPain()
    {
        int rand = Random.Range(0, 2);

        switch(rand)
        {
            case 0:
                pain1.Play();
                break;
            case 1:
                pain2.Play();
                break;
        }
    }

    public void playDrink()
    {
        drink.Play();
    }

    public void playDeepBreath()
    {
        deepBreath.Play();
    }

    public void playScribble()
    {
        scribble.Play();
    }

    public void playMunch()
    {
        munch.Play();
    }
}
