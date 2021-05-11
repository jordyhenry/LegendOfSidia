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
        }
    }
}