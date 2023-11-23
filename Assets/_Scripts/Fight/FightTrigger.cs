using Assets._Scripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.Fight
{
    public class FightTrigger : MonoBehaviour
    {
        public event Action FightStarted;

        public bool IsFightStarted { get; private set; } = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                IsFightStarted = true;
                FightStarted?.Invoke();
            }
        }
    }
}