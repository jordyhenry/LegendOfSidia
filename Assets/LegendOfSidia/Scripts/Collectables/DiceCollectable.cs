using UnityEngine;

namespace LegendOfSidia
{
    public class DiceCollectable : CollectableWithCanvas
    {
        public override void Collect(Player player)
        {
            base.Collect(player);
            player.dices++;
        }
    }
}