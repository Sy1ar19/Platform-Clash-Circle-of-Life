using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class SoundVolumeHandler : MonoBehaviour
    {
        private const string MusicVolumeKey = "MusicVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _effectsAudioSource;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;

        private readonly float maxVolume = 1f;
        private readonly float minVolume = 0f;
        private bool _isAdPlaying = false;

        private void Start()
        {
            InitializeSlider(_musicSlider, _musicAudioSource, MusicVolumeKey);
            InitializeSlider(_effectsSlider, _effectsAudioSource, EffectsVolumeKey);

            _musicSlider.onValueChanged.AddListener(value => SetVolume(_musicAudioSource, value, MusicVolumeKey));
            _effectsSlider.onValueChanged.AddListener(value => SetVolume(_effectsAudioSource, value, EffectsVolumeKey));
        }

        private void InitializeSlider(Slider slider, AudioSource audioSource, string volumeKey)
        {
            float savedVolume = PlayerPrefs.GetFloat(volumeKey, maxVolume);
            slider.value = savedVolume;
            SetVolume(audioSource, savedVolume, volumeKey);
        }

        private void SetVolume(AudioSource audioSource, float volume, string volumeKey)
        {
            volume = Mathf.Clamp(volume, minVolume, maxVolume);
            UpdateAudioSourceVolume(audioSource, volume);
            PlayerPrefs.SetFloat(volumeKey, volume);
            PlayerPrefs.Save();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus && !_isAdPlaying)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
        }

        public void Mute()
        {
            UpdateAudioSourceVolume(_musicAudioSource, minVolume);
            UpdateAudioSourceVolume(_effectsAudioSource, minVolume);
        }

        public void Unmute()
        {
            SetVolume(_musicAudioSource, PlayerPrefs.GetFloat(MusicVolumeKey, maxVolume), MusicVolumeKey);
            SetVolume(_effectsAudioSource, PlayerPrefs.GetFloat(EffectsVolumeKey, maxVolume), EffectsVolumeKey);
        }

        public void SetAdIsPlaying(bool isPlaying)
        {
            _isAdPlaying = isPlaying;
        }

        private void UpdateAudioSourceVolume(AudioSource audioSource, float volume)
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }
}