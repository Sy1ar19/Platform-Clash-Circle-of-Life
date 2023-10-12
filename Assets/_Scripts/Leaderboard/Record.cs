using TMPro;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetScore(string score)
    {
        _score.text = score;
    }
}