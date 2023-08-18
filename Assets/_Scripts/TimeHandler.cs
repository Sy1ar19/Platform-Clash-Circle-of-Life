using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
