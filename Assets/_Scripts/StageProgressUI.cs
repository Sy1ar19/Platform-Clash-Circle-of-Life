using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressUI : MonoBehaviour
{
    private const string CompletedLevelsKey = "CompletedLevels";

    [SerializeField] private List<Button> _levelButtons;
    [SerializeField] private List<Renderer> _bossBody;
    [SerializeField] private List<Renderer> _bossHead;
    [SerializeField] private Material _headCompletedMaterial;
    [SerializeField] private Material _bodyCompletedMaterial;
    [SerializeField] private Material lockedMaterial;

    private void Start()
    {
        UpdateButtonsAvailability(SaveLoadSystem.LoadData(CompletedLevelsKey, 0));
    }

    public void UpdateButtonsAvailability(int availableLevels)
    {
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].interactable = (i < availableLevels + 1);

            Button button = _levelButtons[i];
            Renderer body = _bossBody[i];
            Renderer head = _bossHead[i];

            if (i < availableLevels + 1)
            {
                body.material = _bodyCompletedMaterial;
                head.material = _headCompletedMaterial;
            }
            else
            {
                body.material = lockedMaterial;
                head.material = lockedMaterial;
            }

        }
    }
}
