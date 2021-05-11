using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia {
    public class SquaredTilesBoard : Board
    {
        public Material whiteMaterial;
        public Material blackMaterial;
        public override void CreateBoard()
        {
            tiles = new Tile[rows, columns];
            bool useWhiteMaterial = false;
            for (int i = 0; i < rows; i++)
            {
                useWhiteMaterial = !useWhiteMaterial;
                for (int j = 0; j < columns; j++)
                {
                    Material tileMaterial = GetNextMaterial(useWhiteMaterial);
                    useWhiteMaterial = !useWhiteMaterial;
                    Tile tile = CreateNewTile(i, j, tileMaterial);
                    tiles[i, j] = tile;
                }
            }
        }

        public override List<Tile> GetAdjacentTiles(int x, int y)
        {
            List<Tile> adjacents = new List<Tile>();

            if (!isOutOfBounds(x + 1, y))
                adjacents.Add(tiles[x + 1, y]);
            
            if (!isOutOfBounds(x - 1, y))
                adjacents.Add(tiles[x - 1, y]);
            
            if (!isOutOfBounds(x, y + 1))
                adjacents.Add(tiles[x, y + 1]);
            
            if (!isOutOfBounds(x, y - 1))
                adjacents.Add(tiles[x, y - 1]);

            return adjacents;
        }

        private Tile CreateNewTile (int x, int y, Material mat)
        {
            Vector3 tilePosition = GenerateTilePosition(x, y);
            GameObject tileGO = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            Tile tile = tileGO.GetComponent<Tile>();
            tile.ApplyMaterial(mat);
            tile.coords = new TileCoords(x, y);

            return tile;
        }

        private Material GetNextMaterial(bool useWhiteMaterial) => (useWhiteMaterial) ? whiteMaterial : blackMaterial;

        private Vector3 GenerateTilePosition (int x, int y) => new Vector3(x, transform.position.y, y);
    }
}