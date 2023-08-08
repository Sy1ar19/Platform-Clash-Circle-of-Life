using System;
using System.Collections;
using UnityEngine;

public class BossFinalFightHandler : MonoBehaviour
{
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _transitionTime = 1.0f;
    [SerializeField] private Boss _boss;
    [SerializeField] private GameObject _victoryCanvas;

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
        _boss.BossDied += OnBossDied;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;
        _boss.BossDied -= OnBossDied;
    }

    private void OnBossDied()
    {
        _player.PlayVictoryAnimation();
        StartCoroutine(ShowVictoryCanvasAfterDelay());

    }

    private IEnumerator ShowVictoryCanvasAfterDelay()
    {
        yield return new WaitForSeconds(_transitionTime);
        _victoryCanvas.SetActive(true);
    }

    private void OnFightStarted()
    {
        Debug.Log("Fight");
        _player.TryGetComponent<Rigidbody>(out Rigidbody playerRigidbody);
        _player.StopMove();
    }
}
