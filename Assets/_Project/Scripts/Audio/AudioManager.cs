using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source Settings")]
    [SerializeField] List<Sounds> _soundsList;
    [SerializeField] AudioSource _audioSource;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

    }

    public void PlaySound(int soundID)
    {
        var sound = _soundsList.Find(s => s.ID == soundID);

        if (sound == null)
        {
            Debug.LogWarning($"Sound '{soundID}' not found!");
            return;
        }

        _audioSource.PlayOneShot(sound.Clip);
    }

    [Serializable]
    public class Sounds
    {
        public int ID;
        public AudioClip Clip;
    }

    public void DoorMovement()
    {
        AudioManager.instance.PlaySound(1);
    }
}
