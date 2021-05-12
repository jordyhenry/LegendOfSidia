using UnityEngine;

namespace LegendOfSidia
{
    public abstract class Collectable : MonoBehaviour
    {
        protected AudioSource audioSource;
        public ParticleSystem particlesSystem;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void Collect(Player player)
        {
            if(audioSource) audioSource.Play();
            if(particlesSystem) particlesSystem.Play();
        }

        protected private void Die() => Destroy(gameObject);
    }
}