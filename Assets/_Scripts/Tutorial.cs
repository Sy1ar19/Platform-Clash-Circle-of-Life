using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private const string TutorialCompletedKey = "TutorialCompleted";

    [SerializeField] private TimeHandler _timeHandler;
    [SerializeField] GameObject[] tutorialPanels;

    private void Start()
    {
        if (PlayerPrefs.HasKey(TutorialCompletedKey) == false)
        {
            ShowTutorial();
            _timeHandler.Pause();
        }
        else
        {
            HideTutorial();
        }
    }

    public void ShowTutorial()
    {
        foreach (var panel in tutorialPanels)
        {
            panel.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        foreach (var panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save();
    }
}
