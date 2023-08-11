using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _player;

    private void OnEnable()
    {
        _player.SetActive(true);
    }
}
