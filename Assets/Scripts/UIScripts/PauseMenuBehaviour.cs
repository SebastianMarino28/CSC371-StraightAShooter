using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PauseMenuBehaviour : MonoBehaviour
{
    private Canvas pauseMenu;
    public Boolean paused;
    public Stack<Canvas> menuStack;
    public UIButtonBehaviour buttons;

    void Awake() {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<Canvas>();
        menuStack = new Stack<Canvas>();
        paused = false;
    }

    public void Pause(InputValue pauseValue)
    {
        if (pauseValue.isPressed)
        {
            if(!paused)
                PausePushPop(pauseMenu);
            else
                if(menuStack.Count == 1 && menuStack.Peek().name == "StatsScreen") {
                    PausePushPop(null);
                    PausePushPop(pauseMenu);
                }
                else
                {
                    PausePushPop(null);
                }    
                          
        }
    }

    public void PausePushPop(Canvas canvas) {
        if(canvas != null) {
            if(menuStack.Count == 0) {
                paused = true;
                Time.timeScale = 0f;
            }
            AddAndEnable(canvas);  
        }
        else {
            buttons.abilityButton.interactable = false;
            buttons.sliderDamage.interactable = false;
            buttons.sliderSpeed.interactable = false;
            buttons.sliderMusic.interactable = false;
            buttons.sliderSounds.interactable = false;
            buttons.invincibilityToggle.interactable = false;
            PopAndDisable();
            if(menuStack.Count == 0) {
                paused = false;
                Time.timeScale = 1f;
            }
        }
    }

    // Disables the current canvas, then pushes and enables the chosen canvas
    private void AddAndEnable(Canvas canvas) {
        if(menuStack.Count > 0) {
            Canvas tos = menuStack.Peek();
            tos.enabled = false;
        }
        menuStack.Push(canvas);
        canvas.enabled = true;
    }

    // Disables and pops the current canvas, then enables the next most recent canvas
    private void PopAndDisable() {
        Canvas tos = menuStack.Peek();
        tos.enabled = false;
        menuStack.Pop();
        if(menuStack.Count > 0) {
            Canvas next = menuStack.Peek();
            next.enabled = true;
        }
    }
}
