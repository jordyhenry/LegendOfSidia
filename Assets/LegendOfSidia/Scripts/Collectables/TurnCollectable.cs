using UnityEngine;

namespace LegendOfSidia
{
    public class TurnCollectable : CollectableWithCanvas
    {
        public override void Collect(Player player)
        {
            base.Collect(player);
            player.turns++;
        }
    }
}