using Agava.YandexGames;
using UnityEngine;

public class Autorization : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void Authorize()
    {
        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.Authorize(RequestPersonalProfileDataPermission);
        }
        else
        {
            OpenPanel();
        }

    }

    private void RequestPersonalProfileDataPermission()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    private void OpenPanel()
    {
        _panel.SetActive(true);
    }
}
