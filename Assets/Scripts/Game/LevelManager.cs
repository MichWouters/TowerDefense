using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class LevelManager : MonoBehaviour
    {
        public void LoadStartMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}