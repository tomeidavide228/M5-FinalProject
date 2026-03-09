using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI_Options : MonoBehaviour
{
    [Header("Option Menu Settings")]
    [SerializeField] private GameObject _Menu;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private UnityEngine.UI.Slider _masterSlider;
    [SerializeField] private UnityEngine.UI.Slider _soundSlider;
    [SerializeField] private UnityEngine.UI.Slider _musicSlider;

    private void Start()
    {
        float master = PlayerPrefs.GetFloat("Master", 1f);
        float sound = PlayerPrefs.GetFloat("Sound", 1f);
        float music = PlayerPrefs.GetFloat("Music", 1f);

        _masterSlider.value = master;
        _soundSlider.value = sound;
        _musicSlider.value = music;

        SetVolume(master, "Master");
        SetVolume(sound, "Sound");
        SetVolume(music, "Music");
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("Master", value);
        SetVolume(value, "Master");
    }

    public void SetSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("Sound", value);
        SetVolume(value, "Sound");
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
        SetVolume(value, "Music");
    }


    private void SetVolume(float value, string group)
    {
        if (value > 0.01f)
        {
            var volume = Mathf.Log10(value) * 20;
            _mixer.SetFloat(group, volume);
        }
        else
        {
            _mixer.SetFloat(group, -80f);
        }
    }

    public void BackClicked()
    {
        MenuState.OptionClosed();
        gameObject.SetActive(false);
        _Menu.SetActive(true);
        Debug.Log("Return Back");
    }
}
