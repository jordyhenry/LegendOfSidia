using UnityEngine;
using UnityEngine.SceneManagement;

namespace LegendOfSidia
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;
        //public GameOverController.GameOverSceneData gameOverData;

        private const string MENU_SCENE_NAME = "Menu";
        private const string GAMEPLAY_SCENE_NAME = "Gameplay";
        private const string GAMEOVER_SCENE_NAME = "GameOver";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

        }

        public void LoadMenu() => LoadScene(MENU_SCENE_NAME);
        public void LoadGameplay() => LoadScene(GAMEPLAY_SCENE_NAME);
        public void LoadGameOver() => LoadScene(GAMEOVER_SCENE_NAME);

        private void LoadScene(string newScene)
        {
            SceneManager.LoadScene(newScene);
        }
    }
}