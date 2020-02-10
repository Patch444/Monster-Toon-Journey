using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    // Sets up an int to track how far the player has gotten.
    public int howFar;

    // sets up a float to keep track of the SFX volume.
    public float sfxVolume;

    public Scene currentScene;
    public string sceneName;

    public Slider SFXslider;
    public Slider MUSslider;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        // Prevents the Global Manager from being destroyed between scenes.
        DontDestroyOnLoad(this.gameObject);
        Screen.SetResolution(1600, 900, true);


    }

    // Update is called once per frame
    void Update()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Settings")
        {
            SFXslider = GameObject.Find("MUSSlider").GetComponent<Slider>();
            SFXslider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

            SFXslider = GameObject.Find("SFXSlider").GetComponent<Slider>();
            SFXslider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);

        mixer.SetFloat("MusVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

}
