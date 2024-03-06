using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuBehaviour : MonoBehaviour
{
    private Canvas pauseMenu;
    public Stack<Canvas> menuStack;

    void Awake() {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<Canvas>();
        menuStack = new Stack<Canvas>();
    }
    void OnPause(InputValue pauseValue)
    {
        if (pauseValue.isPressed)
        {
            if(!pauseMenu.enabled) {
                AddAndEnable(pauseMenu);  
                Time.timeScale = 0f;
            }
            else {
                PopAndDisable();
                if(menuStack.Count == 0) {
                    Time.timeScale = 1f;
                }
            }
        }
        Debug.Log(menuStack.Count);
    }

    public void AddAndEnable(Canvas canvas) {
        if(menuStack.Count > 0) {
            Canvas tos = menuStack.Peek();
            tos.enabled = false;
        }
        menuStack.Push(canvas);
        canvas.enabled = true;
    }

    public void PopAndDisable() {
        Canvas tos = menuStack.Peek();
        tos.enabled = false;
        menuStack.Pop();
        if(menuStack.Count > 0) {
            Canvas next = menuStack.Peek();
            tos.enabled = true;
        }
    }
}
