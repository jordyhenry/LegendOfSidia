using UnityEngine;

namespace LegendOfSidia
{
    public class Highlighter : MonoBehaviour
    {
        public Color highlightedColor;
        public Color hoveredColor;
        
        private Material material;
        private Color normalColor;
        public bool isHighlighted = false;

        private void Start()
        {
            material = GetComponent<Renderer>().material;
            normalColor = material.color;
        }

        public void ActivateHighlight()
        {
            material.color = highlightedColor;
            isHighlighted = true;
        }
        public void DeactivateHighlight()
        {
            material.color = normalColor;
            isHighlighted = false;
        }

        public void HoverIn() => material.color = hoveredColor;
        public void HoverOut() => material.color = (isHighlighted) ? highlightedColor : normalColor;
    }
}