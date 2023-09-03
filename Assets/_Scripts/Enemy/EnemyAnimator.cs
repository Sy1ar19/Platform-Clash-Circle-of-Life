using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerHealth _player;

    public readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    public readonly int IsDying = Animator.StringToHash(nameof(IsDying));
    public readonly int Win = Animator.StringToHash(nameof(Win));

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }
    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        PlayWinAnimation();
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    public void PlayAttackAnimation(bool isAttacking)
    {
        _animator.SetBool(IsAttacking, isAttacking);
    }

    public void PlayWinAnimation()
    {
        _animator.SetBool(Win, true);
    }

    public void PlayDieAnimation()
    {
        _animator.SetBool(IsDying, true);
    }
}
