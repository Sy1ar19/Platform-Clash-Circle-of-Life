using UnityEngine;

public class EffectUtils
{
    public static void PerformEffect(ParticleSystem muzzleEffect, AudioSource audioSource, AudioClip audioClip)
    {
        if (muzzleEffect != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            muzzleEffect.Play();
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(audioClip);
        }
    }
}
