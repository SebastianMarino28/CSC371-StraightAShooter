using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public SFXManager sFXManager;
    public TextMeshProUGUI text;
    public Slider slider;
    public AudioSource musicSource1;
    public AudioSource musicSource2;

    public void OnChangeMusicVolume() {
        text.text = (slider.value * 1000).ToString("0");
        text.text += "%";
        editMusicSourceValue(musicSource1);
        editMusicSourceValue(musicSource2);
    }

    private void editMusicSourceValue(AudioSource source) {
        source.volume = slider.value;
    }

    public void OnChangeSoundsVolume() {
        text.text = (slider.value * 100).ToString("0");
        text.text += "%";
    }

    public void editSoundSourceValue(AudioSource source) {
        source.volume = slider.value;
    }
}
