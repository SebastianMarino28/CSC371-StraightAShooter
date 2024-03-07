using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeBehaviour : MonoBehaviour
{
    private Animator anim;
    private SFXManager sfxManager;
    public InputActionReference inputAction;

    public void Awake() {
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            sfxManager.playAha();
            Time.timeScale = 0f;
            inputAction.action.Disable();
            anim.Play("UpgradeFadeIn");
            Destroy(gameObject);
        }
    }
}
