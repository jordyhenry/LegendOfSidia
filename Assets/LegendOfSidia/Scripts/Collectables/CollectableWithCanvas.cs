using UnityEngine;

namespace LegendOfSidia
{
    public class CollectableWithCanvas : Collectable
    {
        public GameObject canvas;

        public override void Collect(Player player)
        {
            base.Collect(player);
            canvas.SetActive(false);
            Invoke("Die", Mathf.Max(audioSource.clip.length, particlesSystem.main.duration));
        }
    }
}
