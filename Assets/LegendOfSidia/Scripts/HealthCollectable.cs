using UnityEngine;

namespace LegendOfSidia
{
    public class HealthCollectable : Collectable
    {
        private int amount = 0;

        private void Start()
        {
            amount = Random.Range(10, 30);
        }

        public override void Collect(Player player)
        {
            base.Collect(player);

            player.health += amount;
            Invoke("Die", Mathf.Max(audioSource.clip.length, particlesSystem.main.duration));
        }
    }
}