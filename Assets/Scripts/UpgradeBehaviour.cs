using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{
    private Animator anim;
    private SFXManager sfxManager;

    public void Awake() {
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            sfxManager.playAha();
            Time.timeScale = 0.0000001f;
            anim.Play("UpgradeFadeIn");
            Destroy(gameObject);
        }
    }
}
