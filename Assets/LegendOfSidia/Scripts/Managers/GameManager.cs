using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class GameManager : MonoBehaviour
    {
        public enum GAME_STATE
        {
            CREATE_NEW_GAME = 0,
            CHOOSE_NEXT_PLAYER = 1,
            WAIT_PLAYER_MOVE = 2,
            BATTLING = 3,
        }

        private const int STARTING_DICES = 3;
        private const int STARTING_TURNS = 3;

        [Header("MANAGERS")]
        public Board board;
        public CollectablesPlacer collectablesPlacer;
        public TileHighligtherManager tileHighligther;
        public BattleManager battleManager;
        [Space(30)]

        [Header("PLAYER SETUP")]
        public Player.PlayerData[] playersData;
        public GameObject playerPrefab;
        private Player[] players;
        private int currentPlayerIndex = 0;
        [Space(30)]

        [Header("CAMERA COMPONENTS")]
        public CameraRaycaster tileRaycaster;
        public SmoothCameraFollow smoothCameraFollow;
        private GAME_STATE currentState = GAME_STATE.CREATE_NEW_GAME;

        #region CREATE_NEW_GAME
        public void CreateNewGame()
        {
            board.CreateBoard();
            CreatePlayers();
            collectablesPlacer.CreateCollectables(board.GetEmptyTiles());
            ChangeState(GAME_STATE.CHOOSE_NEXT_PLAYER);
        }

        private void CreatePlayers ()
        {
            players = new Player[playersData.Length];

            for (int i = 0; i < playersData.Length; i++)
            {
                Player.PlayerData data = playersData[i];
                Player player = Instantiate(playerPrefab).GetComponent<Player>();
                Player.PlayerUIStats uiStats = (i == 0) ? GameplayUIStatsContainer.Instace.uiPlayer1 : GameplayUIStatsContainer.Instace.uiPlayer2;
                player.Setup(data, STARTING_TURNS, STARTING_DICES, uiStats);
                
                Tile tile = board.GetTile(data.currentTile);
                tile.PlaceContent(player);
                
                players[i] = player;
            }
        }
        #endregion //CREATE_NEW_GAME

        #region CHOOSE_NEXT_PLAYER
        private void ChooseNextPlayer ()
        {
            Player currentPlayer = players[currentPlayerIndex];

            if (currentPlayer.turns <= 0)
            {
                currentPlayer.ResetTurnPoints(STARTING_TURNS, STARTING_DICES);
                currentPlayerIndex++;
                if (currentPlayerIndex >= players.Length) currentPlayerIndex = 0;
            }

            currentPlayer = players[currentPlayerIndex];
            smoothCameraFollow.target = currentPlayer.transform;
            EnablePlayerMovement(currentPlayer);
        }

        private void EnablePlayerMovement (Player currentPlayer)
        {
            List<Tile> playerAdjacentTiles = board.GetAdjacentTiles(currentPlayer.currentTile.x, currentPlayer.currentTile.y);
            tileHighligther.HighlightItems(playerAdjacentTiles);
            tileRaycaster.onSelectTile += OnPlayerSelectTile;
            ChangeState(GAME_STATE.WAIT_PLAYER_MOVE);
        }
        #endregion //CHOOSE_NEXT_PLAYER

        #region EXECUTE_PLAYER_MOVEMENT
        public void OnPlayerSelectTile(Tile nextTile) 
        {
            tileRaycaster.onSelectTile -= OnPlayerSelectTile;
            Player currentPlayer = players[currentPlayerIndex];

            CollectCollectable(currentPlayer, nextTile);
            MovePlayerToNextTile(currentPlayer, nextTile);

            // Check Battle
            Player adversary = LookForNeighbourPlayer(currentPlayer);
            if (adversary)
            {
                battleManager.onBattleEnd += OnBattleEnd;
                battleManager.StartBattle(currentPlayer, adversary);
                ChangeState(GAME_STATE.BATTLING);
            }
            else
            {
                ChangeState(GAME_STATE.CHOOSE_NEXT_PLAYER);
            }
        }
        private void CollectCollectable(Player currentPlayer, Tile tile)
        {
            if (tile.isEmpty()) return;
            
            Collectable collectable = tile.content.GetComponent<Collectable>();
            if (!collectable) return;

            collectable.Collect(currentPlayer);
            bool shouldRefillBoard = collectablesPlacer.DecreaseCollectablesCount();
            if (shouldRefillBoard)
                collectablesPlacer.CreateCollectables(board.GetEmptyTiles());
        }

        private void MovePlayerToNextTile (Player player, Tile nextTile)
        {
            player.turns--;
            Tile playerCurrentTile = board.GetTile(player.currentTile);
            playerCurrentTile.RemoveContent();
            player.currentTile = nextTile.coords;
            nextTile.PlaceContent(player);
            player.UpdateUI();
        }

        private Player LookForNeighbourPlayer (Player currentPlayer)
        {
            List<Tile> adjacentTiles = board.GetAdjacentTiles(currentPlayer.currentTile.x, currentPlayer.currentTile.y);
            foreach (Tile tile in adjacentTiles)
            {
                if (tile?.content is Player)
                {
                    return (Player)tile.content;
                }
            }

            return null;
        }
        #endregion //EXECUTE_PLAYER_MOVEMENT

        private void OnBattleEnd() 
        {
            battleManager.onBattleEnd -= OnBattleEnd;
            if (players[0].health <= 0)
            {
                SceneLoader.Instance.LoadGameOver();
            }
            else if (players[1].health <= 0)
            {
                SceneLoader.Instance.LoadGameOver();
            }

            ChangeState(GAME_STATE.CHOOSE_NEXT_PLAYER);
        }


        #region STATE_MANAGEMENT
        private void ChangeState (GAME_STATE newState)
        {
            currentState = newState;
        }

        private void Update()
        {
            switch (currentState)
            {
                case GAME_STATE.CREATE_NEW_GAME:
                    CreateNewGame();
                    break;
                case GAME_STATE.CHOOSE_NEXT_PLAYER:
                    ChooseNextPlayer();
                    break;
                case GAME_STATE.WAIT_PLAYER_MOVE:
                    break;
                case GAME_STATE.BATTLING:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}