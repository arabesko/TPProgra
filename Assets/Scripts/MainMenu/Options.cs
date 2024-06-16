using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider sliderMusic;
    public Slider sliderEfects;
    public Toggle toggleMusic;
    public Toggle toggleSound;
    void Start()
    {
        float musicVolume;
        mixer.GetFloat("MusicVol", out musicVolume);
        sliderMusic.value = musicVolume;

        float soundVolume;
        mixer.GetFloat("EfectsVol", out soundVolume);
        sliderEfects.value = soundVolume;
    }

    public void OnVolumeMusicChange()
    {
        float musicVolume = sliderMusic.value;
        mixer.SetFloat("MusicVol", musicVolume);

        toggleMusic.isOn = musicVolume > -80;
    }

    public void OnVolumeSoundChange()
    {
        float soundVolume = sliderEfects.value;
        mixer.SetFloat("EfectsVol", soundVolume);

        toggleSound.isOn = soundVolume > -80;
    }

    public void OnToggleMusic()
    {
        if (sliderMusic.value == -80)
        {
            toggleMusic.isOn = false;
            return;
        }
        if (toggleMusic.isOn)
        {
            mixer.SetFloat("MusicVol", sliderMusic.value);
        }
        else
        {
            mixer.SetFloat("MusicVol", -80f);
        }
    }
    public void OnToggleSound()
    {
        if(sliderEfects.value == -80)
        {
            toggleSound.isOn = false;
            return;
        }
        if (toggleSound.isOn)
        {
            mixer.SetFloat("EfectsVol", sliderEfects.value);
        }
        else
        {
            mixer.SetFloat("EfectsVol", -80f);
        }
    }
}
