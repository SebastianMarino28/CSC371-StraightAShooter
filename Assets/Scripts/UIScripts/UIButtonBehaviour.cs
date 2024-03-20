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
    // [7] = HowToPlayScreen
    // [8] = HaungsMode
    private Animator anim;
    private SFXManager sfxManager;

    [Header("Player Vars")]
    public PlayerController player;

    [Header("Stat UI")]
    public UnityEngine.UI.Image defenseBar;
    public UnityEngine.UI.Image damageBar;
    public UnityEngine.UI.Image speedBar;

    public GameObject defenseUpgrade;
    public GameObject damageUpgrade;
    public GameObject speedUpgrade;

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
        speedUpgradeAmt = .04f;
    }

    public void HaungsModeClick()
    {
        Canvas haungsCanvas = screens[8].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(haungsCanvas);
    }

    public void HowToPlayButtonClick() {
        Canvas howToPlayCanvas = screens[7].GetComponent<Canvas>();
        pauseMenuScript.PausePushPop(howToPlayCanvas);
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
        if (!player.inCombat)
        {
            player.transform.position = homePoint;
            cam.transform.position = cameraPoint;
            pauseMenuScript.PausePushPop(null);       // disable pause screen
        }    
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
        if (player.defenseLevel < 10)
        {
            // implement mplayer.ax health increase
            sfxManager.playDeepBreath();
            player.defenseLevel += 1;
            player.defense += defenseUpgradeAmount;
            defenseBar.fillAmount = player.defenseLevel / 10f;
            anim.Play("UpgradeFadeOut");
            //pauseAction.action.Enable();
            Time.timeScale = 1;
            if (player.defenseLevel >= 10) { defenseUpgrade.SetActive(false); }
        }
    }
    public void IncreaseSpeed()
    {
        if (player.speedLevel < 10)
        {
            // implement speed increase
            sfxManager.playDrink();
            player.wh.bulletCooldown -= speedUpgradeAmt;
            player.speedLevel += 1;
            speedBar.fillAmount = player.speedLevel / 10f;
            //speed += speedUpgradeAmt;
            anim.Play("UpgradeFadeOut");
            //pauseAction.action.Enable();
            Time.timeScale = 1;
            if (player.speedLevel >= 10) { speedUpgrade.SetActive(false); }
        }
    }
    public void IncreaseDamage()
    {
        if (player.defenseLevel < 10)
        {
            // implement damage increase
            sfxManager.playScribble();
            player.damage += damageUpgradeAmt;
            player.damageLevel += 1;
            damageBar.fillAmount = player.damageLevel / 10f;
            anim.Play("UpgradeFadeOut");
            //pauseAction.action.Enable();
            Time.timeScale = 1;
            if (player.damageLevel >= 10) { damageUpgrade.SetActive(false); }
        }
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

