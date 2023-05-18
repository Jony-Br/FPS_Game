using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private HumanStats _player;

    private void Start()
    {
        _player.OnHPChanged += ChangeHP;
        _player.OnArmorChanged += ChangeArmor;
    }

    private void ChangeHP()
    {
        _healthText.text = _player.Hp.ToString();
    }

    private void ChangeArmor()
    {
        _armorText.text = _player.Armor.ToString();
    }

}
