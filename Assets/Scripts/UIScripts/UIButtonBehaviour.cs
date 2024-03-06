using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBehaviour : MonoBehaviour
{  
    public GameObject[] screens;
    // [0] = UpgradeScreen
    // [1] = GameOverScreen
    // [2] = WinScreen
    // [3] = PauseScreen
    // [4] = OptionsScreen
    // [5] = StatsScreen
    // [6] = FinalTPScreen
    
    private PauseMenuBehaviour pauseMenuScript;

    void Awake() {
        pauseMenuScript = screens[3].GetComponent<PauseMenuBehaviour>();
    }

    public void OptionsButtonClick() {
        Canvas optCanvas = screens[4].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(optCanvas);
    }

    public void StatsButtonClick() {
        Canvas statsCanvas = screens[5].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(statsCanvas);
    }

    public void ReturnHomeButton() {
        Debug.Log("Go home buton idk");
    }

    public void BackButtonClick() {
        pauseMenuScript.PausePushPop(null);
    }

    public void FinalTPButtonClick() {
        Animator anim = screens[6].GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("FinalTPFadeIn");
        Time.timeScale = 0f;
    }

    // TODO: Refactor other UI buttons (GameOver, Win) into here
}

