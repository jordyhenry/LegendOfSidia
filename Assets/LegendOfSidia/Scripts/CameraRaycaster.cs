using UnityEngine;

namespace LegendOfSidia
{
    public class CameraRaycaster : MonoBehaviour
    {
        public Camera raycastCamera;
        public LayerMask raycastLayer;
        public float rayDistance = 30f;

        public delegate void OnSelectTile(Tile tile);
        public event OnSelectTile onSelectTile;

        private Highlighter hovered = null;

        public void Update()
        {
            
            if (Input.GetMouseButtonDown(0) && hovered != null)
            {
                Tile tile = hovered.GetComponent<Tile>();
                if (tile && onSelectTile != null)
                    onSelectTile(tile);
            }
            
            Highlighter hoveredHighlighter = GetHighlighterFromRayCast();
            if (hoveredHighlighter && hoveredHighlighter.isHighlighted)
            {
                if (hoveredHighlighter != hovered && hovered != null)
                {
                    hovered.HoverOut();
                }
                hovered = hoveredHighlighter;
                hovered.HoverIn();
            }
            else
            {
                ClearHovered();
            }
        }

        private void ClearHovered()
        {
            hovered?.HoverOut();
            hovered = null;

        }

        private Highlighter GetHighlighterFromRayCast ()
        {
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo, rayDistance, raycastLayer))
            {
                Highlighter highlighter = hitInfo.collider.GetComponent<Highlighter>();
                return highlighter;
            }

            return null;
        }
    }
}