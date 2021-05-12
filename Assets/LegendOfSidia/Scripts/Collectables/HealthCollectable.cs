using UnityEngine;

namespace LegendOfSidia
{
    public class HealthCollectable : CollectableWithCanvas
    {
        public Vector2 healthBonusMinMax;

        public override void Collect(Player player)
        {
            base.Collect(player);

            player.turnBonusHealth += Mathf.RoundToInt(Random.Range(healthBonusMinMax.x, healthBonusMinMax.y));
            player.UpdateUI();
        }
    }
}