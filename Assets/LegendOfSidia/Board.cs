using UnityEngine;

namespace LegendOfSidia
{
    public abstract class Board : MonoBehaviour
    {
        [Range(8, 32)]
        public int columns = 16;
        [Range(8, 32)]
        public int rows = 16;

        public GameObject tilePrefab;
        public abstract void CreateBoard();
    }
}
