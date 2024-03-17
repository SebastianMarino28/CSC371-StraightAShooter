using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBehaviour : MonoBehaviour
{  
    private PauseMenuBehaviour pauseMenuScript;
    public GameObject[] screens;
    // [0] = UpgradeScreen
    // [1] = GameOverScreen
    // [2] = WinScreen
    // [3] = PauseScreen
    // [4] = OptionsScreen
    // [5] = StatsScreen (inventory/backpack)
    // [6] = FinalTPScreen
    // [7] = HowToPlayScreen (Not implemented yet)
    private Animator anim;
    private SFXManager sfxManager;

    [Header("Player Vars")]
    public PlayerController player;

    [Header("Upgrade Values")]
    public static float defenseUpgradeAmount;   // 0.06 as of commit 421b5f0
    public static float damageUpgradeAmt;       // 0.25 as of commit 421b5f0
    public static float speedUpgradeAmt;        // 0.03 as of commit 421b5f0
    
    [Header("TP Vars")]
    public Vector3 homePoint;
    public GameObject cam;
    public Vector3 cameraPoint;
    

    void Awake() {
        pauseMenuScript = screens[3].GetComponent<PauseMenuBehaviour>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        anim = GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<Animator>();

        defenseUpgradeAmount = .06f;
        damageUpgradeAmt = .25f;
        speedUpgradeAmt = .03f;
    }

    public void OptionsButtonClick() {
        Canvas optCanvas = screens[4].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(optCanvas);
    }

    public void BackpackClick() {
        Canvas statsCanvas = screens[5].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(statsCanvas);
    }

    public void ReturnHomeButton() {
        //player.transform.position = homePoint;
        //cam.transform.position = cameraPoint;
        //pauseMenuScript.PausePushPop(null);       // disable pause screen
    }

    public void BackButtonClick() {
        pauseMenuScript.PausePushPop(null);
    }

    public void FinalTPButtonClick() {
        Animator anim = screens[6].GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("FinalTPFadeIn");
        Time.timeScale = 0f;
    }


    /* -=-=- Upgrade buttons -=-=- */

    public void IncreaseDefense()
    {
        // implement mplayer.ax health increase
        sfxManager.playDeepBreath();
        player.defenseLevel += 1;
        anim.Play("UpgradeFadeOut");
        //pauseAction.action.Enable();
        Time.timeScale = 1;
    }
    public void IncreaseSpeed()
    {
        // implement speed increase
        sfxManager.playDrink();
        player.wh.bulletCooldown -= speedUpgradeAmt;
        //speed += speedUpgradeAmt;
        anim.Play("UpgradeFadeOut");
        //pauseAction.action.Enable();
        Time.timeScale = 1;
    }
    public void IncreaseDamage()
    {
        // implement damage increase
        sfxManager.playScribble();
        player.damage += damageUpgradeAmt;
        anim.Play("UpgradeFadeOut");
        //pauseAction.action.Enable();
        Time.timeScale = 1;
    }
    public void Heal(float healAmount)
    {
        if (player.curHealth < player.maxHealth)
        {
            player.curHealth += healAmount;
        }
        if (player.curHealth > player.maxHealth)
        {
            player.curHealth = player.maxHealth;
        }

        sfxManager.playMunch();
        player.healthBar.fillAmount = player.curHealth / player.maxHealth;


        anim.Play("UpgradeFadeOut");
        //pauseAction.action.Enable();
        Time.timeScale = 1;
    }

    // TODO: Refactor other UI buttons (GameOver, Win) into here
}

