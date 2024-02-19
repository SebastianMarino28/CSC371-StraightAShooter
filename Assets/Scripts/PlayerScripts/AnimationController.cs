using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animation animator;
    PlayerController pc;

    private void Start()
    {
        animator = GetComponent<Animation>();
        pc = GetComponent<PlayerController>();
    }

}
