using UnityEngine;

public class FightHandler : MonoBehaviour
{
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _transitionTime = 1.0f;

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;
    }

    private void OnFightStarted()
    {
        Debug.Log("Fight");
        _player.TryGetComponent<Rigidbody>(out Rigidbody playerRigidbody);
        _player.StopMove();
    }
}
