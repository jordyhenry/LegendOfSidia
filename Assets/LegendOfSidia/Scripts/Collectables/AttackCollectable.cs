using UnityEngine;

namespace LegendOfSidia
{
    public class AttackCollectable : CollectableWithCanvas
    {
        public Vector2 attackBonusMinMax;
        public override void Collect(Player player)
        {
            base.Collect(player);

            player.turnBonusAttack += Mathf.RoundToInt(Random.Range(attackBonusMinMax.x, attackBonusMinMax.y));
            player.UpdateUI();
        }
    }
}