using UnityEngine;

namespace LegendOfSidia
{
    public class Tile : MonoBehaviour
    {
        public Board.TileCoords coords;
        private MonoBehaviour content = null;

        public void PlaceContent (MonoBehaviour _content)
        {
            Transform contentTransform = _content.transform;

            contentTransform.position = transform.position;
            contentTransform.rotation = Quaternion.identity;
            contentTransform.parent = this.transform;

            content = _content;
        }

        public bool isEmpty() => content == null;
        public void ApplyMaterial(Material newMat) => GetComponent<Renderer>().material = newMat;
    }
}