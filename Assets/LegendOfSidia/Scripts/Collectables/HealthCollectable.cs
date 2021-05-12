using UnityEngine;

namespace LegendOfSidia
{
    public class HealthCollectable : Collectable
    {
        private int amount = 0;
        public GameObject canvas;

        private void Start()
        {
            amount = Random.Range(10, 30);
        }

        public override void Collect(Player player)
        {
            base.Collect(player);

            canvas.SetActive(false);
            player.turnBonusHealth += amount;
            Invoke("Die", Mathf.Max(audioSource.clip.length, particlesSystem.main.duration));
        }
    }
}