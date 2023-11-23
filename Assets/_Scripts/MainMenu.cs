using UnityEngine;

namespace Assets._Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        private void OnEnable()
        {
            _player.SetActive(true);
        }
    }
}