using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTPButtons : MonoBehaviour
{
    public void YesButtonClick() {
        Debug.Log("TELEPORT TO FINAL ROOM HERE");
    }
    
    public void NoButtonClick() {
        Time.timeScale = 1f;
        Animator anim = GameObject.FindGameObjectWithTag("FinalTPScreen").GetComponent<Animator>();
        anim.enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}

