using UnityEngine;

namespace LegendOfSidia {
    public class SquaredTilesBoard : Board
    {
        public Material whiteMaterial;
        public Material blackMaterial;
        public override void CreateBoard()
        {
            bool useWhiteMaterial = false;
            for (int i = 0; i < rows; i++)
            {
                useWhiteMaterial = !useWhiteMaterial;
                for (int j = 0; j < columns; j++)
                {
                    Material tileMaterial = GetNextMaterial(useWhiteMaterial);
                    useWhiteMaterial = !useWhiteMaterial;
                    CreateNewTile(i, j, tileMaterial);
                }
            }
        }

        private void CreateNewTile (int x, int y, Material mat)
        {
            Vector3 tilePosition = GenerateTilePosition(x, y);
            GameObject tileGO = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            Tile tile = tileGO.GetComponent<Tile>();
            tile.ApplyMaterial(mat);
            tile.coords = new TileCoords(x, y);
        }

        private Material GetNextMaterial(bool useWhiteMaterial) => (useWhiteMaterial) ? whiteMaterial : blackMaterial;

        private Vector3 GenerateTilePosition (int x, int y) => new Vector3(x, transform.position.y, y);
    }
}