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
        if (Screen.width > Screen.height)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
