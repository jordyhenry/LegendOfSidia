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
                    Vector3 tilePosition = new Vector3(i, transform.position.y, j);
                    GameObject tileGO = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                    tileGO.GetComponent<Renderer>().material = (useWhiteMaterial) ? whiteMaterial : blackMaterial;
                    useWhiteMaterial = !useWhiteMaterial;
                }
            }
        }
    }
}