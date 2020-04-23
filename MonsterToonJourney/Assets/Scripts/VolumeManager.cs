using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;
    public Slider volume;
    public Slider fxVolume;
    public AudioMixer musMixer;
    public AudioMixer fxMixer;
    // Start is called before the first frame update
    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        fxVolume.value = PlayerPrefs.GetFloat("FxVolume", 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        //music.volume = volume.value;
        //sfx.volume = fxVolume.value;
    }

    public void VolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", volume.value);
        PlayerPrefs.SetFloat("FxVolume", fxVolume.value);
        Debug.Log("MusicVol " + PlayerPrefs.GetFloat("MusicVolume"));
        Debug.Log("SoundVol " + PlayerPrefs.GetFloat("FxVolume"));
    }
}
