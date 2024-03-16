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
    // [5] = StatsScreen (inventory/backpack)
    // [6] = FinalTPScreen
    // [7] = HowToPlayScreen (Not implemented yet)
    public GameObject player;
    public Vector3 homePoint;
    public GameObject cam;
    public Vector3 cameraPoint;
    
    private PauseMenuBehaviour pauseMenuScript;

    void Awake() {
        pauseMenuScript = screens[3].GetComponent<PauseMenuBehaviour>();
    }

    public void OptionsButtonClick() {
        Canvas optCanvas = screens[4].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(optCanvas);
    }

    public void BackpackClick() {
        Canvas statsCanvas = screens[5].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(statsCanvas);
    }

    public void ReturnHomeButton() {
        //player.transform.position = homePoint;
        //cam.transform.position = cameraPoint;
        //pauseMenuScript.PausePushPop(null);       // disable pause screen
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

