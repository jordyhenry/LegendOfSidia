using UnityEngine;

namespace LegendOfSidia {
    public class SquaredTilesBoard : Board
    {
        public override void CreateBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 tilePosition = new Vector3(i, transform.position.y, j);
                    Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                }
            }
        }
    }
}