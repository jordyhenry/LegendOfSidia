using UnityEngine;
using UnityEngine.UI;

namespace LegendOfSidia
{
    public class GameOverManager : MonoBehaviour
    {
        public struct GameOverSceneData
        {
            public Color winnerColor;
            public string winnerName;
            public Color loserColor;

            public GameOverSceneData(Color winnerColor, string winnerName, Color loserColor)
            {
                this.winnerColor = winnerColor;
                this.winnerName = winnerName;
                this.loserColor = loserColor;
            }
        }

        public Renderer winnerRenderer;
        public Renderer loserRenderer;
        public Text winnerNameText;
        public void Start()
        {
            GameOverSceneData data = SceneLoader.Instance.gameOverData;
            winnerRenderer.material.color = data.winnerColor;
            loserRenderer.material.color = data.loserColor;
            winnerNameText.text = data.winnerName;
        }

        public void LoadMenu() => SceneLoader.Instance.LoadMenu();
        public void LoadGameplay() => SceneLoader.Instance.LoadGameplay();
    }
}