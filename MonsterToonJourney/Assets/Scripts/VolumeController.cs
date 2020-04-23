using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class VolumeController : MonoBehaviour
{
    public AudioMixer musMixer;
    public AudioMixer fxMixer;
    // Start is called before the first frame update
    void Start()
    {
        musMixer.SetFloat("MusVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        fxMixer.SetFloat("SoundVol", Mathf.Log10(PlayerPrefs.GetFloat("FxVolume")) * 20);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
