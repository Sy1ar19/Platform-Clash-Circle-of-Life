using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class YandexInitialize : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_EDITOR_test
        OnInitialize();
        
#endif
        YandexGamesSdk.CallbackLogging = true;
    }


#if UNITY_WEBGL && !UNITY_EDITOR
/*    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }*/

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
