using UnityEngine;

namespace LegendOfSidia
{
    public class Tile : MonoBehaviour
    {
        public Board.TileCoords coords;

        public void ApplyMaterial(Material newMat) => GetComponent<Renderer>().material = newMat;
    }
}