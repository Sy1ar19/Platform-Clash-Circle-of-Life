using Agava.YandexGames;
using UnityEngine;

public class Advertisement : MonoBehaviour
{
    [SerializeField] private AdvertisementButton _advertisementButton;
    [SerializeField] private SoundVolumeController _soundVolumeController;


    private bool _adIsPlaying;

    public bool AdIsPlaying => _adIsPlaying;

    private static bool _hasAdPlayedInStart = false;

    private void Start()
    {
/*#if UNITY_WEBGL && !UNITY_EDITOR
        if (!_hasAdPlayedInStart)
        {
            PlayAd();
            _hasAdPlayedInStart = true;
        }
#endif*/
    }

    public void OnShowVideoButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnPlayed, OnRewarded,OnClosed);
#endif
    }

    private void OnRewarded()
    {
        _soundVolumeController.Mute();
        _advertisementButton.WatchAd();
    }

    private void OnClosed()
    {
        _soundVolumeController.SetAdIsPlaying(false);
        _soundVolumeController.Unmute();
        _adIsPlaying = false;
    }

    private void OnPlayed()
    {
        _soundVolumeController.SetAdIsPlaying(true);
        _soundVolumeController.Mute();
        _adIsPlaying = true;
    }

    private void ShowInterstitialAd()
    {
        InterstitialAd.Show(OnPlayed, OnClosedInterstitialAd);
    }

    private void PlayRegularAdIf(bool value)
    {
        if (value)
        {
            ShowInterstitialAd();
        }
    }

    private void PlayAd()
    {
        ShowInterstitialAd();
    }

    private void OnClosedInterstitialAd(bool value)
    {
        _soundVolumeController.Unmute();
        _adIsPlaying = false;
    }
}
