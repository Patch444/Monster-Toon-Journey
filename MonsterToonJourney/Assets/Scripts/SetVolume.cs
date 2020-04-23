using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;
    public GlobalManager gm;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SFXVolume");
        //mixer.SetFloat("SoundVol", PlayerPrefs.GetFloat("SFXVolume"));
        gm = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        //gm.sfxVolume = slider.value;
    }
}
