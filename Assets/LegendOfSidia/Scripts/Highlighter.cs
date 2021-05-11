using UnityEngine;

namespace LegendOfSidia
{
    public class Highlighter : MonoBehaviour
    {
        private Material material;
        private Color normalColor;

        public Color highlightedColor;

        private void Start()
        {
            material = GetComponent<Renderer>().material;
            normalColor = material.color;
        }

        public void ActivateHighlight() => material.color = highlightedColor;
        public void DeactivateHighlight() => material.color = normalColor;
    }
}