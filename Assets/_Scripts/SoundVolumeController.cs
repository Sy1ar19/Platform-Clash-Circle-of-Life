using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    private const string VolumeKey = "Volume";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _slider;

    private float _volume;
    private float maxVolume = 1f;

    private void Start()
    {
        _volume = SaveLoadSystem.LoadData(VolumeKey, maxVolume);

        _audioSource.volume = _volume;

        _slider.value = _volume;
    }

    private void LateUpdate()
    {
        if (_audioSource.volume != _slider.value)
        {
            _audioSource.volume = _slider.value;
            _volume = _slider.value;
           SaveLoadSystem.SaveData(VolumeKey, _volume);
        }
    }
}

