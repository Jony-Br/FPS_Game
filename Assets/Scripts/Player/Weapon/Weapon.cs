using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private WeaponModel _weaponModel;

    [SerializeField] private float _damage;
    public float Damage { get => _damage; set => _damage = value; }
    [SerializeField] private float _reloadTime;
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    [SerializeField] private float _periodBetweenShots;
    public float PeriodBetweenShots { get => _periodBetweenShots; set => _periodBetweenShots = value; }
    [SerializeField] private float _weight;
    public float Weight { get => _weight; set => _weight = value; }
    [SerializeField] private int _bulletsInMagazine;
    public int BulletsInMagazine { get => _bulletsInMagazine;set => _bulletsInMagazine = value; }
    [SerializeField] private int _maximumBulletsInMagazine;
    public int MaximumBulletsInMagazine { get => _maximumBulletsInMagazine; set => _maximumBulletsInMagazine = value; }
    [SerializeField] private int _bulletsLeft;
    public int BulletsLeft { get => _bulletsLeft; set => _bulletsLeft = value; }


    enum WeaponType
    {
        Main,
        Secondary,
        Melee,
        Grenade
    }
    enum WeaponModel
    {
        Ak_47,
        M4a4,
        usp,
        glock
    }
}
