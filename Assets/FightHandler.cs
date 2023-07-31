using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;
    }
    private void OnFightStarted()
    {
        throw new System.NotImplementedException();
    }
}
