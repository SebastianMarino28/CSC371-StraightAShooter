using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenBehaviour : MonoBehaviour
{   
    public Boolean isFadingIn;
    public Boolean isFadingOut;
    public float fadeTime; 
    private float alpha;
    private float alphaStep;
    
    void Awake() {
        isFadingIn = false;
        isFadingOut = false;
        fadeTime = 1f;
        alpha = 0f;
        alphaStep = .0125f;
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
        }
        GetComponent<CanvasGroup>().alpha = alpha;
    }
}
