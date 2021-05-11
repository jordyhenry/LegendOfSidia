using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class GameManager : MonoBehaviour
    {
        private const int STARTING_DICES = 3;
        private const int STARTING_TURNS = 3;

        [Header("MANAGERS")]
        public Board board;
        public CollectablesPlacer collectablesPlacer;
        public TileHighligtherManager tileHighligther;


        [Header("PLAYER SETUP")]
        public Player.PlayerData[] playersData;
        public GameObject playerPrefab;
        private Player[] players;
        [Space(30)]



        public CameraRaycaster tileRaycaster;

        public void StartGame()
        {
            board.CreateBoard();
            CreatePlayers();
            // Setup Player UI
            collectablesPlacer.CreateCollectables(board.GetEmptyTiles());
        }

        private void CreatePlayers ()
        {
            players = new Player[playersData.Length];

            for (int i = 0; i < playersData.Length; i++)
            {
                Player.PlayerData data = playersData[i];
                Player player = Instantiate(playerPrefab).GetComponent<Player>();
                player.Setup(data, STARTING_TURNS, STARTING_DICES);
                
                Tile tile = board.GetTile(data.currentTile);
                tile.PlaceContent(player);
                
                players[i] = player;
            }
        } 

        private void PlacePlayer (Tile tile)
        {
            //player.position = tile.transform.position;
            List<Tile> adjacentTiles = board.GetAdjacentTiles(tile.coords.x, tile.coords.y);
            tileHighligther.HighlightItems(adjacentTiles);
        }
    }
}