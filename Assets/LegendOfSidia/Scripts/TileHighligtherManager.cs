using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class TileHighligtherManager : MonoBehaviour
    {
        List<Highlighter> highlighters = new List<Highlighter>();

        private void DeactivateHighlights ()
        {
            foreach (Highlighter high in highlighters)
            {
                high?.DeactivateHighlight();
            }
            highlighters.Clear();
        }

        public void HighlightItems (List<Tile> tiles)
        {
            DeactivateHighlights();
            foreach (Tile t in tiles)
            {
                Highlighter high = t.GetComponent<Highlighter>();
                
                if (high)
                {
                    high.ActivateHighlight();
                    highlighters.Add(high);
                }
            }
        }
    }
}