using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string MainMenu = "MainMenu";
    private const string Stage1 = "Stage1";
    private const string Stage2 = "Stage2";

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void OpenStage1()
    {
        SceneManager.LoadScene(Stage1);
    }

    public void OpenStage2()
    {
        SceneManager.LoadScene(Stage2);
    }
}
