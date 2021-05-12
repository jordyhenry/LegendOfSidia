using UnityEngine;

namespace LegendOfSidia
{
    public class Player : MonoBehaviour
    {
        [System.Serializable]
        public struct PlayerData
        {
            public string name;
            public Board.TileCoords currentTile;
            public Color color;
            public int health;
            public int attack;
        }

        public Renderer _renderer;
        
        public Board.TileCoords currentTile;
        public int health;
        public int attack;
        public int turns;
        public int dices;
        public Color color;

        public void Setup (PlayerData data, int _turns, int _dices)
        {
            currentTile = data.currentTile;
            transform.name = data.name;
            health = data.health;
            attack = data.attack;
            color = data.color;
            turns = _turns;
            dices = _dices;

            _renderer.material.color = color;
        }

        public void ResetTurnPoints(int _turns, int _dices)
        {
            Debug.Log("Reset Turn Points");
            turns = _turns;
            dices = _dices;
            /*
                currentPlayer.health -= turnExtraPoints.health;
                currentPlayer.attack -= turnExtraPoints.attack;
             */
        }
    }
}