using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTPButtons : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void YesButtonClick() {
        Time.timeScale = 1f;
        Animator anim = GameObject.FindGameObjectWithTag("FinalTPScreen").GetComponent<Animator>();
        anim.enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        player.canPause = true;
    }
    
    public void NoButtonClick() {
        Time.timeScale = 1f;
        Animator anim = GameObject.FindGameObjectWithTag("FinalTPScreen").GetComponent<Animator>();
        anim.enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        player.canPause = true;
    }
}

