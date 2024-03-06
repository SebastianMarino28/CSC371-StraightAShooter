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
    
    private PauseMenuBehaviour pauseMenuScript;

    void Awake() {
        pauseMenuScript = screens[3].GetComponent<PauseMenuBehaviour>();
    }

    public void OptionsButtonClick() {
        Canvas optCanvas = screens[4].GetComponent<Canvas>();
        pauseMenuScript.AddAndEnable(optCanvas);
    }

    public void StatsButtonClick() {
        Canvas statsCanvas = screens[5].GetComponent<Canvas>();
        pauseMenuScript.AddAndEnable(statsCanvas);
    }

    public void BackButtonClick() {
        pauseMenuScript.PopAndDisable();
    }
}

