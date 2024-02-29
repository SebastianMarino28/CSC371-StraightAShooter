using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{
    public GameObject upgradeScreen;

    private AudioSource upgradeSound;
    void Awake() {
        upgradeScreen = GameObject.FindGameObjectWithTag("UpgradeScreen");
        upgradeSound = gameObject.GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            upgradeSound.Play();
            upgradeScreen.GetComponent<UpgradeScreenBehaviour>().isFadingIn = true;
            Time.timeScale = 0.0000001f;
            Destroy(gameObject);
        }
    }
}
