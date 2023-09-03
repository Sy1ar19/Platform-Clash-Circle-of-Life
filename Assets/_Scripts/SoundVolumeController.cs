using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string EffectsVolumeKey = "EffectsVolume";

    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;

    private float maxVolume = 1f;

    private void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, maxVolume);
        float savedEffectsVolume = PlayerPrefs.GetFloat(EffectsVolumeKey, maxVolume);

        _musicSlider.value = savedMusicVolume;
        _effectsSlider.value = savedEffectsVolume;

        UpdateAudioSourceVolume(_musicAudioSource, savedMusicVolume);
        UpdateAudioSourceVolume(_effectsAudioSource, savedEffectsVolume);

        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        UpdateAudioSourceVolume(_musicAudioSource, volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetEffectsVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        UpdateAudioSourceVolume(_effectsAudioSource, volume);
        PlayerPrefs.SetFloat(EffectsVolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void UpdateAudioSourceVolume(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
