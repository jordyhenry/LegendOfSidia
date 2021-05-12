using UnityEngine;
using UnityEngine.UI;

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

        [System.Serializable]
        public struct PlayerUIStats
        {
            public Text healthText;
            public Text turnsText;
            public Text attakcText;
        }

        public Renderer _renderer;
        
        public Board.TileCoords currentTile;
        public int health;
        public int attack;
        public int turns;
        public int dices;
        public Color color;

        public int turnBonusHealth = 0;
        public int turnBonusAttack = 0;

        public PlayerUIStats UIStats;

        public void Setup (PlayerData data, int _turns, int _dices, PlayerUIStats _UIStats)
        {
            currentTile = data.currentTile;
            transform.name = data.name;
            health = data.health;
            attack = data.attack;
            color = data.color;
            turns = _turns;
            dices = _dices;

            UIStats = _UIStats;
            _renderer.material.color = color;

            UpdateUI();
        }

        public void ResetTurnPoints(int _turns, int _dices)
        {
            Debug.Log("Reset Turn Points");
            turns = _turns;
            dices = _dices;

            turnBonusAttack = 0;
            turnBonusHealth = 0;

            UpdateUI();
            /*
                currentPlayer.health -= turnExtraPoints.health;
                currentPlayer.attack -= turnExtraPoints.attack;
             */
        }

        public void UpdateUI()
        {
            UIStats.attakcText.text = (attack + turnBonusAttack).ToString().PadLeft(2);
            UIStats.healthText.text = (health + turnBonusHealth).ToString().PadLeft(2);
            UIStats.turnsText.text = (turns).ToString().PadLeft(2);
        }
    }
}