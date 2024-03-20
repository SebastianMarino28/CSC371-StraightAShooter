using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicSelector : MonoBehaviour
{
    public AudioSource bgm1;
    public AudioSource bgm2;
    public AudioSource bossMusic;
    public bool bossFight = false;

    private int selector;

    private void Start()
    {
        selector = Random.Range(0, 2);
    }
    void Update()
    {
        if(!bgm1.isPlaying && !bgm2.isPlaying && !bossFight)
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

    public void StartBossMusic()
    {
        bossFight = true;
        if (bgm1.isPlaying)
        {
            bgm1.Stop();
        }
        if (bgm2.isPlaying)
        {
            bgm2.Stop();
        }
        bossMusic.Play();
    }
}
