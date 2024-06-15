using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Audio")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioMixer _mixer;

    [Header("UI")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private float _initMasterVol = .5f;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private float _initMusicVol = .5f;
    [SerializeField] private Slider _efectsSlider;
    [SerializeField] private float _initEfectsVol = .1f;

    private void Start()
    {
        _masterSlider.value = _initMasterVol;
        SetMasterVolume(_initMasterVol);
        _musicSlider.value = _initMusicVol;
        SetMusicVolume(_initMusicVol);
        _efectsSlider.value = _initEfectsVol;
        SetEfectsVolume(_initEfectsVol);
    }

    public void SetMasterVolume(float value)
    {
        _mixer.SetFloat("MasterVol", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        _mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }

    public void SetEfectsVolume(float value)
    {
        _mixer.SetFloat("EfectsVol", Mathf.Log10(value) * 20);
    }

    public void PlayAudio(AudioClip clip)
    {
        if (clip == _source.clip) return;

        _source.Stop();
        _source.clip = clip;
        _source.Play();
    }
}
