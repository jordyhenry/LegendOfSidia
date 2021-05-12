using UnityEngine;

namespace LegendOfSidia
{
    public class MenuManager : MonoBehaviour
    {
        public void LoadGameplay()
        {
            SceneLoader.Instance.LoadGameplay();
        }
    }
}