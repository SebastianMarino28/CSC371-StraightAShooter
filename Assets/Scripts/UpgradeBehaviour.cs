using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeBehaviour : MonoBehaviour
{
    private Animator anim;
    private SFXManager sfxManager;
    private PlayerController player;
    public InputActionReference pauseAction;

    [Header("Float behaviour")]
    public float heightOffset;
    public float moveSpeed;
    private float moveDir = 1;

    public void Awake() {
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * moveDir * Time.deltaTime), transform.position.z);
        if (transform.position.y >= heightOffset || transform.position.y <= -heightOffset)
        {
            moveDir *= -1;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            sfxManager.playAha();
            Time.timeScale = 0f;
            //pauseAction.action.Disable();
            player.canPause = false;
            anim.Play("UpgradeFadeIn");
            Destroy(gameObject);
        }
    }

    // green for upgrade get: 43CF2E
}
