using Agava.YandexGames;
using UnityEngine;

namespace Assets._Scripts.Yandex
{
    public class Autorization : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private CanvasGroup _rankCanvas;
        [SerializeField] private GameObject _agreementPanel;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Authorize()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                PlayerAccount.Authorize();
            }
            else
            {
                RequestPersonalProfileDataPermission();
                OpenPanel();
            }
        }

        private void RequestPersonalProfileDataPermission()
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        }

        private void OpenPanel()
        {
            _rankCanvas.alpha = 1;
            _rankCanvas.blocksRaycasts = true;
            _rankCanvas.interactable = true;
        }

        public void CheckAutorization()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                _agreementPanel.SetActive(true);
                _canvasGroup.interactable = false;
            }
            else
            {
                OpenPanel();
            }
        }
    }
}