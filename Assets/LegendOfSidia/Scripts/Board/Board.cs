using System.Collections.Generic;
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

        private protected Tile[,] tiles;

        public GameObject tilePrefab;
        public abstract void CreateBoard();
        public abstract List<Tile> GetAdjacentTiles(int x, int y);
        private protected bool isOutOfBounds(int x, int y) => (x < 0 || x >= rows || y < 0 || y >= columns);

        public Tile GetTile(int x, int y) => tiles[x, y];
        public Tile GetTile(TileCoords coords) => tiles[coords.x, coords.y];

        public List<Tile> GetEmptyTiles()
        {
            List<Tile> emptyTiles = new List<Tile>();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tile currentTile = tiles[i, j];
                    if (currentTile.isEmpty())
                        emptyTiles.Add(currentTile);
                }
            }

            return emptyTiles;
        }
    }
}
