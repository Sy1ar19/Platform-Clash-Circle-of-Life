using System;
using UnityEngine;

public class FinalFightHandler : MonoBehaviour
{
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _transitionTime = 1.0f;
    [SerializeField] private Boss _boss;

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
        _boss.Died += OnBossDied;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;
        _boss.Died -= OnBossDied;
    }

    private void OnBossDied()
    {
        _player.PlayVictoryAnimation();
    }

    private void OnFightStarted()
    {
        Debug.Log("Fight");
        _player.TryGetComponent<Rigidbody>(out Rigidbody playerRigidbody);
        _player.StopMove();
    }
}
