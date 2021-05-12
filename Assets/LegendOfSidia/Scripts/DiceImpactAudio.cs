using UnityEngine;

namespace LegendOfSidia
{
    public class DiceImpactAudio : MonoBehaviour
    {
        private AudioSource audioSource;

        public AudioClip[] impactAudios;
        public float minVelocity = 3f;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude >= minVelocity)
            {
                int clipIndex = Random.Range(0, impactAudios.Length);
                audioSource.clip = impactAudios[clipIndex];
                audioSource.Play();
            }
        }
    }
}