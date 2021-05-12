using System.Collections;
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

        public IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            uiPlayer1.backgroundImage.gameObject.SetActive(false);
            uiPlayer1.backgroundImage.gameObject.SetActive(true);
        }

        public Player.PlayerUIStats uiPlayer1;
        public Player.PlayerUIStats uiPlayer2;
        public GameObject blurPanel;
    }
}