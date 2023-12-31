﻿using Agava.YandexGames;
using Assets._Scripts.UI;
using UnityEngine;

namespace Assets._Scripts.Yandex
{
    public class Advertisement : MonoBehaviour
    {
        [SerializeField] private AdvertisementButton _advertisementButton;
        [SerializeField] private SoundVolumeHandler _soundVolumeController;

        private bool _adIsPlaying;

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
}