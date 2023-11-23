using UnityEngine;

namespace Assets._Scripts.UI
{
    public class EffectUtils
    {
        private const float MinInclusive = 0.8f;
        private const float MaxInclusive = 1.2f;

        public static void PerformEffect(ParticleSystem muzzleEffect, AudioSource audioSource, AudioClip audioClip)
        {
            if (muzzleEffect != null)
            {
                PerformEffectOnComponent(muzzleEffect, audioSource, audioClip);
            }

            if (audioSource != null && audioClip != null)
            {
                PerformEffectOnComponent(audioSource, audioSource, audioClip);
            }
        }

        private static void PerformEffectOnComponent(Component component, AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.pitch = Random.Range(MinInclusive, MaxInclusive);

            if (component is ParticleSystem particleSystem)
            {
                particleSystem.Play();
            }
            else if (audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}