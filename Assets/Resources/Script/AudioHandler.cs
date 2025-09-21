using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)]
    public float volume = 1;
    public bool loop;
}
public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;
    public AudioSource musicSource;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        instance = this;
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        
        PlayMusic("MainMenu");
    }
    public Sound[] musicList;
    public Sound[] sfxList;

    void Update()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        if (Input.GetMouseButtonDown(0))
        {
            PlaySFX("MouseClick");
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = System.Array.Find(sfxList, s => s.name == name);
        if (sound != null)
        {
            GameObject audioObject = new GameObject(name);
            audioObject.transform.SetParent(transform);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.volume = sfxSlider.value;
            audioSource.loop = sound.loop;
            audioSource.Play();
            if (!sound.loop)
            {
                Destroy(audioObject,sound.clip.length + 0.5f);
            }
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = System.Array.Find(musicList, s => s.name == name);
        if (sound != null && musicSource.isPlaying)
        {
            Debug.Log("switching music");
            StartCoroutine(SwitchMusic(sound));
        }
        else if (sound != null && !musicSource.isPlaying)
        {
            musicSource.clip = sound.clip;
            // musicSource.volume = sound.volume;
            musicSource.loop = sound.loop;
            musicSource.Play();
        }
    }

    IEnumerator SwitchMusic(Sound sound)
    {
        Debug.Log("decreasing volume");
        float targetVolume = musicSlider.value;
        // Fade out
        while (musicSource.volume > 0f)
        {
            musicSource.volume = Mathf.Max(0f, musicSource.volume - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        musicSource.volume = 0f;
        musicSource.clip = sound.clip;
        musicSource.loop = sound.loop;
        musicSource.Play();
        Debug.Log("increasing volume");
        while (musicSource.volume < targetVolume)
        {
            musicSource.volume = Mathf.Min(targetVolume, musicSource.volume + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        musicSource.volume = targetVolume;
    }

    public void StopSFX(string name)
    {
        GameObject audioObject = GameObject.Find(name);
        if (audioObject != null)
        {
            Destroy(audioObject);
        }
    }

    public void StopAllSFX()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Utilities

    public void SetMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public bool isMusicPlaying(string name)
    {
        Sound sound = System.Array.Find(musicList, s => s.name == name);
        if (sound != null && musicSource.clip == sound.clip && musicSource.isPlaying)
        {
            return true;
        }
        return false;
    }
}
