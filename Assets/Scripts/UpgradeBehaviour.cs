using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{

    public GameObject upgradeScreen;

    void Awake() {
        upgradeScreen = GameObject.FindGameObjectWithTag("UpgradeScreen");
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            upgradeScreen.GetComponent<UpgradeScreenBehaviour>().isFadingIn = true;
            Time.timeScale = 0.0000001f;
            Destroy(gameObject);
        }
    }
}
