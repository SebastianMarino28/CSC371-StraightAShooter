using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicSelector : MonoBehaviour
{
    public AudioSource bgm1;
    public AudioSource bgm2;

    public int selector = 0;

    void Update()
    {
        if(!bgm1.isPlaying && !bgm2.isPlaying)
        {
            selector++;
            selector %= 2;

            if (selector == 0)
            {
                bgm1.Play();
            }
            else if (selector == 1)
            {
                bgm2.Play();
            }
        }      
    }
}
