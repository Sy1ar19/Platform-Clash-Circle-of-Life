using UnityEngine;

namespace Assets._Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
        private readonly int IsDying = Animator.StringToHash(nameof(IsDying));
        private readonly int Win = Animator.StringToHash(nameof(Win));

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAttackAnimation(bool isAttacking)
        {
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
}