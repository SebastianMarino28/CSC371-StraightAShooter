using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenBehaviour : MonoBehaviour
{   
    public Boolean isFadingIn;
    public Boolean isFadingOut;
    public GameObject[] upgradeButtons;
    public float fadeTime; 
    private float alpha;
    private float alphaStep;
    
    void Awake() {
        isFadingIn = false;
        isFadingOut = false;
        upgradeButtons = new GameObject[4];
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
            foreach(GameObject g in upgradeButtons) {
                g.GetComponentInChildren<UnityEngine.UI.Button>().enabled = true;
            }
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
