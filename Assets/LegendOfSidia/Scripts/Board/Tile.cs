using UnityEngine;

namespace LegendOfSidia
{
    public class Tile : MonoBehaviour
    {
        public Board.TileCoords coords;
        public MonoBehaviour content = null;

        public void PlaceContent (MonoBehaviour _content)
        {
            Transform contentTransform = _content.transform;

            contentTransform.position = transform.position;
            contentTransform.rotation = Quaternion.identity;
            contentTransform.parent = this.transform;

            content = _content;
        }

        public void RemoveContent ()
        {
            if (!content) return;
            content.transform.parent = null;
            content = null;
        }

        public bool isEmpty() => content == null;
        public void ApplyMaterial(Material newMat) => GetComponent<Renderer>().material = newMat;
    }
}