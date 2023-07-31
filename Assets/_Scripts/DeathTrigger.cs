using UnityEngine;

public class DeahtTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Die();
        }
    }
}
