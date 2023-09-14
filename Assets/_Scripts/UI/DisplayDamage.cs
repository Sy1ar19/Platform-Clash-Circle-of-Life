using DamageNumbersPro;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] private DamageNumber _popupPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Weapon _weapon;

    private const string CriticalHitText = "!";

    public void SpawnPopup(float number)
    {
        DamageNumber newPopup = _popupPrefab.Spawn(_target.transform.position, number); 
        newPopup.SetFollowedTarget(_target.transform);

        if (_weapon != null && _weapon.IsCriticalHit)
        {
            newPopup.SetScale(10f);

            newPopup.enableRightText = true;
            newPopup.rightText = CriticalHitText;

            newPopup.SetColor(new Color(1, 0.2f, 0.2f));
        }
        else
        {
            newPopup.SetScale(10);
            newPopup.enableRightText = false;
            newPopup.SetColor(new Color(1, 0.7f, 0.5f));
        }
    }
}
