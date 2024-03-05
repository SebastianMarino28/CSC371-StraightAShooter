using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{
    public GameObject upgradeScreen;
    public SFXManager sfxManager;

    void Awake() {
        upgradeScreen = GameObject.FindGameObjectWithTag("UpgradeScreen");
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            sfxManager.playAha1();
            upgradeScreen.GetComponent<UpgradeScreenBehaviour>().isFadingIn = true;
            Time.timeScale = 0.0000001f;
            Destroy(gameObject);
        }
    }
}
