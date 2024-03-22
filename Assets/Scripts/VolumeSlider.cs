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
    public AudioSource musicSource3;

    public void OnChangeMusicVolume() {
        text.text = (slider.value * 1000).ToString("0");
        text.text += "%";
        EditMusicSourceValue(musicSource1);
        EditMusicSourceValue(musicSource2);
        EditMusicSourceValue(musicSource3);
    }

    private void EditMusicSourceValue(AudioSource source) {
        source.volume = slider.value;
    }

    public void OnChangeSoundsVolume() {
        text.text = (slider.value * 100).ToString("0");
        text.text += "%";
    }

    public void EditSoundSourceValue(AudioSource source) {
        source.volume = slider.value;
    }
}
