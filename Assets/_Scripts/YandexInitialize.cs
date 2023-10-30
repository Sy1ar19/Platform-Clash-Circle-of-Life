using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class YandexInitialize : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_EDITOR_test
#endif
        YandexGamesSdk.CallbackLogging = true;
    }

#if UNITY_WEBGL 
    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialize);
    }
#endif

    private void OnInitialize()
    {
        SceneManager.LoadScene(1);
    }
}
