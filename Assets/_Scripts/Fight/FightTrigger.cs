using System;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    public event Action FightStarted;

    public bool IsFightStarted { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            IsFightStarted = true;
            FightStarted?.Invoke();
        }
    }
}
