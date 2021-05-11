using UnityEngine;

namespace LegendOfSidia
{
    public abstract class Board : MonoBehaviour
    {
        [System.Serializable]
        public struct TileCoords
        {
            public int x;
            public int y;

            public TileCoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [Range(8, 32)]
        public int columns = 16;
        [Range(8, 32)]
        public int rows = 16;

        public GameObject tilePrefab;
        public abstract void CreateBoard();
    }
}
