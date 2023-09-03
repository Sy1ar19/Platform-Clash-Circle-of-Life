using System.Collections;
using UnityEngine;

public class BossFinalFightHandler : MonoBehaviour
{
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private BossHealth _boss;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerMover _playerMover;


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
        _playerAnimator.PlayVictoryAnimation();
        StartCoroutine(ShowVictoryCanvasAfterDelay());
    }

    private IEnumerator ShowVictoryCanvasAfterDelay()
    {
        yield return new WaitForSeconds(_transitionTime);
        _victoryCanvas.SetActive(true);
    }

    private void OnFightStarted()
    {
        _playerMover.StopMove();
        _playerAnimator.PlayAttackAnimation(true);
    }
}
