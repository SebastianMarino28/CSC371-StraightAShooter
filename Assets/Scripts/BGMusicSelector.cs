using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicSelector : MonoBehaviour
{
    public AudioSource bgm1;
    public AudioSource bgm2;

    private int selector;

    private void Start()
    {
        selector = Random.Range(0, 2);
    }
    void Update()
    {
        if(!bgm1.isPlaying && !bgm2.isPlaying)
        {
            selector++;
            selector %= 2;

            if (selector == 0)
            {
                bgm1.Play();
                bgm1.volume = .25f;
            }
            else if (selector == 1)
            {
                bgm2.Play();
                bgm2.volume = .25f;
            }
        }      
    }
}
