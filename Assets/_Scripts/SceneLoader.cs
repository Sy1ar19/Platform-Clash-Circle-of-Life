using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        private const string MainMenu = "MainMenu";
        private const string Stage1 = "Stage1";
        private const string Stage2 = "Stage2";
        private const string Stage3 = "Stage3";
        private const string Stage4 = "Stage4";
        private const string Stage5 = "Stage5";
        private const string Stage6 = "Stage6";

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenu);
        }

        public void LoadStage1()
        {
            SceneManager.LoadScene(Stage1);
        }

        public void LoadStage2()
        {
            SceneManager.LoadScene(Stage2);
        }

        public void LoadStage3()
        {
            SceneManager.LoadScene(Stage3);
        }

        public void LoadStage4()
        {
            SceneManager.LoadScene(Stage4);
        }

        public void LoadStage5()
        {
            SceneManager.LoadScene(Stage5);
        }

        public void LoadStage6()
        {
            SceneManager.LoadScene(Stage6);
        }
    }
}