using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextExample : MonoBehaviour
{
    public DamageNumber numberPrefab;

    void Update()
    {
        // On left-click.
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the numberPrefab is not null.
            if (numberPrefab != null)
            {
                // Spawn new popup at transform.position with a random number between 0 and 100.
                DamageNumber damageNumber = numberPrefab.Spawn(transform.position, Random.Range(1, 100));
            }
        }
    }
}
