using UnityEngine;
using UnityEngine.UI;

public class SettingUU : MonoBehaviour
{
    public Slider sound, music;
    private void Start()
    {
        sound.value = AudioManager.Instance.sfxSource.volume;
        music.value = AudioManager.Instance.musicSource.volume;

        sound.onValueChanged.AddListener(UpdateSoundVolume);
        music.onValueChanged.AddListener(UpdateMusicVolume);
    }
    private void UpdateSoundVolume(float value)
    {
        AudioManager.Instance.sfxSource.volume = value;
    }

    private void UpdateMusicVolume(float value)
    {
        AudioManager.Instance.musicSource.volume = value; 
    }

    public void ButtonExit()
    {
        UIManager.Singleton.CloseUI();
    }
}
