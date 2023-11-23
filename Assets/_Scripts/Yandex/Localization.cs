using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

namespace Assets._Scripts.Yandex
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "en";
        private const string RussianCode = "ru";
        private const string TurkishCode = "tr";
        private const string EnglishLanguage = "English";
        private const string RussianLanguage = "Russian";
        private const string TurkishLanguage = "Turkish";

        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private Image _enImageCheck;
        [SerializeField] private Image _ruImageCheck;
        [SerializeField] private Image _trImageCheck;

        private Image _activeCheckmark;

        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        SwitchLanguageTo(YandexGamesSdk.Environment.i18n.lang);
#endif
        }

        public void SwitchLanguageTo(string code)
        {
            Image newCheckmark = null;

            switch (code)
            {
                case EnglishCode:
                    newCheckmark = _enImageCheck;
                    SetLanguageAndCheckmark(EnglishLanguage, newCheckmark);
                    break;

                case RussianCode:
                    newCheckmark = _ruImageCheck;
                    SetLanguageAndCheckmark(RussianLanguage, newCheckmark);
                    break;

                case TurkishCode:
                    newCheckmark = _trImageCheck;
                    SetLanguageAndCheckmark(TurkishLanguage, newCheckmark);
                    break;

                default:
                    newCheckmark = _enImageCheck;
                    SetLanguageAndCheckmark(EnglishLanguage, newCheckmark);
                    break;
            }


            if (_activeCheckmark != null && _activeCheckmark != newCheckmark)
            {
                _activeCheckmark.gameObject.SetActive(false);
            }

            _activeCheckmark = newCheckmark;
        }

        private void SetLanguageAndCheckmark(string language, Image checkmark)
        {
            _leanLocalization.SetCurrentLanguage(language);
            SetCheckmarkActive(checkmark);
        }

        private void SetCheckmarkActive(Image checkmark)
        {
            if (checkmark != null)
            {
                checkmark.gameObject.SetActive(true);
            }
        }
    }
}