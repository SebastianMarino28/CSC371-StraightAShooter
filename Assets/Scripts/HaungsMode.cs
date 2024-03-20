using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaungsMode : MonoBehaviour
{
    public PlayerController player;
    public float minAttackCooldown;
    private float startingAttackCooldown;

    private void Awake()
    {
        startingAttackCooldown = player.wh.bulletCooldown;
    }
    public void SetInvinicibility()
    {
        player.isInvincible = !player.isInvincible;
    }

    public void ModifyDamage(float value)
    {
        player.damage = value; 
    }

    public void ModifyAttackSpeed(float ratio)
    {
        float modifier = startingAttackCooldown - minAttackCooldown;
        player.wh.bulletCooldown = startingAttackCooldown - (modifier * ratio);
    }
}
