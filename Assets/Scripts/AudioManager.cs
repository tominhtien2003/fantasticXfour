using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private int indexMusicArray = 0;

    private void Start()
    {
        PlayMusic(musicSounds[indexMusicArray].name);
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            indexMusicArray = (indexMusicArray + 1) % musicSounds.Length; 
            PlayMusic(musicSounds[indexMusicArray].name);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s != null)
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
