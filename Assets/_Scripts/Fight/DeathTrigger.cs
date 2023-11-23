using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Fight
{
    public class DeahtTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                player.Die();
            }
        }
    }
}