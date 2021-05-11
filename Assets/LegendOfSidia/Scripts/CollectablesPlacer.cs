using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class CollectablesPlacer : MonoBehaviour
    {
        [Range(0, 100)]
        public int fillPercentage = 50;
        public GameObject[] collectablePrefabs;

        private int collectablesCount = 0;
        private int targetCollectablesCountToRefill = 0;

        public void CreateCollectables (List<Tile> emptyTiles)
        {
            for (int i = 0; i < emptyTiles.Count; i++)
            {
                if (Random.Range(0f, 100f) > fillPercentage) continue;

                Tile currentTile = emptyTiles[i];
                int prefabIndex = Random.Range(0, collectablePrefabs.Length);
                Collectable collectable = Instantiate(collectablePrefabs[prefabIndex]).GetComponent<Collectable>();
                currentTile.PlaceContent(collectable);
                collectablesCount++;
            }

            targetCollectablesCountToRefill = Mathf.RoundToInt(collectablesCount * 0.1f);
        }
    }
}