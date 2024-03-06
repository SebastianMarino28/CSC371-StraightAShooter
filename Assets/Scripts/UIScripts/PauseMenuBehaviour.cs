using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PauseMenuBehaviour : MonoBehaviour
{
    private Canvas pauseMenu;
    private Boolean paused;
    public Stack<Canvas> menuStack;

    void Awake() {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<Canvas>();
        menuStack = new Stack<Canvas>();
        paused = false;
    }
    void OnPause(InputValue pauseValue)
    {
        if (pauseValue.isPressed)
        {
            if(!paused) {
                AddAndEnable(pauseMenu);  
                if(menuStack.Count == 0) {
                    Debug.Log("PAUSED");
                    paused = true;
                    Time.timeScale = 0f;
                }
            }
            else {
                PopAndDisable();
                if(menuStack.Count == 0) {
                    Debug.Log("UNPAUSED");
                    paused = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }

    // Disables the current canvas, then pushes and enables the chosen canvas
    public void AddAndEnable(Canvas canvas) {
        if(menuStack.Count > 0) {
            Canvas tos = menuStack.Peek();
            tos.enabled = false;
        }
        menuStack.Push(canvas);
        canvas.enabled = true;
        Debug.Log(menuStack.Count);
    }

    // Disables and pops the current canvas, then enables the next most recent canvas
    public void PopAndDisable() {
        Canvas tos = menuStack.Peek();
        tos.enabled = false;
        menuStack.Pop();
        if(menuStack.Count > 0) {
            Canvas next = menuStack.Peek();
            tos.enabled = true;
        }
        Debug.Log(menuStack.Count);
    }
}
