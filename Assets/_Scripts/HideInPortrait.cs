using UnityEngine;

public class HideInPortrait : MonoBehaviour
{
    private void Start()
    {
        CheckScreenOrientation();
    }

    private void Update()
    {
        CheckScreenOrientation();
    }

    private void CheckScreenOrientation()
    {
        // Проверяем текущую ориентацию экрана
        if (Screen.width > Screen.height)
        {
            // Если ориентация альбомная, то делаем объект видимым
            gameObject.SetActive(true);
        }
        else
        {
            // Если ориентация портретная, то скрываем объект
            gameObject.SetActive(false);
        }
    }
}
