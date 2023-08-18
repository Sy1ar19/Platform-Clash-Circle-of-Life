using UnityEngine;

public class EffectUtils
{
    public static void PerformEffect(ParticleSystem muzzleEffect, AudioSource audioSource, AudioClip audioClip)
    {
        if (muzzleEffect != null)
        {
            muzzleEffect.Play();
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
