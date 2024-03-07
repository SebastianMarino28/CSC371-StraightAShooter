using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOpacity : MonoBehaviour
{
    public GameObject text;
    public Slider s;
    private CanvasGroup cg;

    void Awake() {
        cg = text.GetComponent<CanvasGroup>();
    }
    public void OnPrank() {
        cg.alpha = s.value;
    }
}

