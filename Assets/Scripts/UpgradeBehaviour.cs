using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{
    private Animator anim;

    public void Awake() {
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Time.timeScale = 0.0000001f;
            anim.Play("UpgradeFadeIn");
            Destroy(gameObject);
        }
    }
}
