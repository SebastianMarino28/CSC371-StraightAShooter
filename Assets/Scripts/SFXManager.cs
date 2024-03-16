using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource bulletHit;
    public AudioSource doorLock;
    public AudioSource gameOver1;
    public AudioSource gameOver2;
    public AudioSource aha1;
    public AudioSource pain1;
    public AudioSource pain2;
    public AudioSource pain3;
    public AudioSource drink;
    public AudioSource deepBreath;
    public AudioSource scribble;
    public AudioSource munch;
    public AudioSource backpackZip1;
    public AudioSource backpackZip2;

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
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                gameOver1.Play();
                break;
            case 1: 
                gameOver2.Play(); 
                break;
        }
    }

    public void playAha()
    {
        aha1.Play();
    }

    public void playPain()
    {
        int rand = Random.Range(0, 3);

        switch(rand)
        {
            case 0:
                pain1.Play();
                break;
            case 1:
                pain2.Play();
                break;
            case 2:
                pain3.Play();
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
    

    public void playBackpackZip()
    {
        int rand = Random.Range(0, 2);

        switch(rand)
        {
            case 0:
                backpackZip1.Play();
                break;
            case 1:
                backpackZip2.Play();
                break;
        }
    }
}
