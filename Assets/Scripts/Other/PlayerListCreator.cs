using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ctBody;
    [SerializeField] private GameObject _tBody;
    [SerializeField] private Transform _ctBotsHolder;
    [SerializeField] private Transform _tBotsHolder;
    [SerializeField] private Transform _ctPlayersHolder;
    [SerializeField] private Transform _tPlayersHolder;
    [SerializeField] private int _ctMaxNumber;
    [SerializeField] private int _tMaxNumber;
    [SerializeField] private ChooseUnit _chooseUnit; // reference vs static action?

    private void Awake()
    {
        _chooseUnit.OnUnitChosen += AddPlayer;
        
    }

    private void AddPlayer(ChooseUnit.Unit unit)
    {
        if (unit == ChooseUnit.Unit.Ct)
        {
            Instantiate(_ctBody, _ctPlayersHolder);
        }
        else
        {
            Instantiate(_tBody, _tPlayersHolder);
        }
        
        //creating bots after players

        for (int i = _tBotsHolder.GetComponentsInChildren<WeaponController>().Length; i < _tMaxNumber; i++)
        {
            Instantiate(_tBody, _tBotsHolder.transform).tag = "Bot";
        }
        for (int i = _ctBotsHolder.GetComponentsInChildren<WeaponController>().Length; i < _ctMaxNumber; i++)
        {
            Instantiate(_ctBody, _ctBotsHolder.transform).tag = "Bot";
        }
    }
}
