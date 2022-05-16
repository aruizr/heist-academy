using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Header("Sound Settings")] 
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider; 
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private float defaultVolume = 0.5f;

    
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private int defaultSensibility = 4;
    public int controllerSensitivity = 4;
    
    [SerializeField] private Toggle invertY;

    [SerializeField] private Slider brightness; 
    

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        AudioListener.volume = generalVolumeSlider.value;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void ResetVolume()
    {
        AudioListener.volume = defaultVolume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);

        generalVolumeSlider.value = defaultVolume;
        musicVolumeSlider.value = defaultVolume;
        sfxVolumeSlider.value = defaultVolume;
    }

    public void SetControllerSensitivity()
    {
        controllerSensitivity = Mathf.RoundToInt(sensitivitySlider.value);
        PlayerPrefs.SetInt("masterSensibility", controllerSensitivity);
    }

    public void InvertY()
    {
        PlayerPrefs.SetInt("InvertY", invertY.isOn ? 1 : 0);
    }

    public void ResetGameplay()
    {
        sensitivitySlider.value = defaultSensibility;
        PlayerPrefs.SetInt("masterSensibility", defaultSensibility);
        
        invertY.isOn = false;
        PlayerPrefs.SetInt("InvertY", 0);
    }
}
