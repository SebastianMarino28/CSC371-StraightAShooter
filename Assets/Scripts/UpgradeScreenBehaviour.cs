using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreenBehaviour : MonoBehaviour
{  
    public Boolean isFadingIn;
    public Boolean isFadingOut;
    public List<GameObject> buttons;
    public float fadeTime; 
    private float alpha;
    private float alphaStep;
   
    void Awake() {
        isFadingIn = false;
        isFadingOut = false;
        fadeTime = 1f;
        alpha = 0f;
        GetComponent<CanvasGroup>().alpha = 0f;
        alphaStep = .03f;
    }


    void Update()
    {
        if(isFadingIn) {
            GetComponent<Canvas>().enabled = true;
            fadeIn();
        }
        if(isFadingOut) {
            fadeOut();
        }
    }


    void fadeIn() {
        if(alpha >= 1f) {
            alpha = 1f;
            isFadingIn = false;
            // set buttons active
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Button>().interactable = true;
            }
        }
        else {
            alpha += alphaStep + fadeTime * Time.deltaTime;
        }
        GetComponent<CanvasGroup>().alpha = alpha;
    }
   
    void fadeOut() {
        if(alpha < 0f) {
            alpha = 0f;
            isFadingOut = false;
            GetComponent<Canvas>().enabled = false;
        }
        else {
            alpha -= alphaStep + fadeTime * Time.deltaTime;
            // set buttons inactive
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
        GetComponent<CanvasGroup>().alpha = alpha;
    }
}

