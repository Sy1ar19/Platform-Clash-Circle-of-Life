using UnityEngine;

public class EffectUtils
{
    private const float MinInclusive = 0.8f;
    private const float MaxInclusive = 1.2f;

    public static void PerformEffect(ParticleSystem muzzleEffect, AudioSource audioSource, AudioClip audioClip)
    {
        if (muzzleEffect != null)
        {
            audioSource.pitch = Random.Range(MinInclusive, MaxInclusive);
            muzzleEffect.Play();
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.pitch = Random.Range(MinInclusive, MaxInclusive);
            audioSource.PlayOneShot(audioClip);
        }
    }
}
