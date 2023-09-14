using UnityEngine;

public class DeahtTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            //player.ApplyDamage(player.CurrentHealth);
            player.Die();
        }
    }
}
