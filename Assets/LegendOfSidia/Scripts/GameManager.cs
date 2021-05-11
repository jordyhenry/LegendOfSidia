using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class GameManager : MonoBehaviour
    {
        public Transform player;
        public CameraRaycaster tileRaycaster;
        public Board board;

        public void Start()
        {
            board.CreateBoard();
            tileRaycaster.onSelectTile += PlacePlayer;
        }

        private void PlacePlayer (Tile tile)
        {
            player.position = tile.transform.position;
            List<Tile> adjacentTiles = board.GetAdjacentTiles(tile.coords.x, tile.coords.y);

            foreach (Tile t in adjacentTiles)
            {
                Highlighter highlighter = t.GetComponent<Highlighter>();
                highlighter?.ActivateHighlight();
            }
        }
    }
}