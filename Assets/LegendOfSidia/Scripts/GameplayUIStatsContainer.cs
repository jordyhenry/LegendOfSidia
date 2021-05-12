using UnityEngine;

namespace LegendOfSidia
{
    public class GameplayUIStatsContainer : MonoBehaviour
    {
        public static GameplayUIStatsContainer Instace;

        public void Awake()
        {
            if (Instace == null)
                Instace = this;
            else if (Instace != this)
                Destroy(gameObject);
        }

        public Player.PlayerUIStats uiPlayer1;
        public Player.PlayerUIStats uiPlayer2;
    }
}