using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Header("Settings")] 
    [SerializeField] private Slider volumeSlider;
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
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log($"Player prefs master volume is: {PlayerPrefs.GetFloat("masterVolume")}");
    }

    public void ResetVolume()
    {
        AudioListener.volume = defaultVolume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log($"Player prefs master volume is: {PlayerPrefs.GetFloat("masterVolume")}");
        volumeSlider.value = defaultVolume;
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
