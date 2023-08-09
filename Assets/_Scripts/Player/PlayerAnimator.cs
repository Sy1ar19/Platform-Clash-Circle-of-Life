using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    public readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    public readonly int IsDying = Animator.StringToHash(nameof(IsDying));
    public readonly int Win = Animator.StringToHash(nameof(Win));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation(bool isAttacking)
    {
        Debug.Log("attack anim");
        _animator.SetBool(IsAttacking, isAttacking);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetBool(IsDying, true);
    }

    public void PlayVictoryAnimation()
    {
        _animator.SetBool(Win, true);
    }
}
