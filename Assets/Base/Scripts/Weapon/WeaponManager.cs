using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private WeaponData currentWeapon;
    [SerializeField] private GameObject player;
    [SerializeField] private Attack attackScript;
    [SerializeField] private Transform weaponHolder;

    void Awake()
    {
        attackScript = GetComponent<Attack>();
    }

    public void WeaponPickup(WeaponData acquiredWeapon)
    {
        currentWeapon = acquiredWeapon;
        ChangeWeaponSprite();
        attackScript.AdjustDamageForWeapon(currentWeapon.weaponDamage);
    }

    private void ChangeWeaponSprite()
    {
        Debug.Log("Weapon sprite changed to: " + currentWeapon.weaponSprite.name);
        weaponHolder = transform.Find("Weapon");
        SpriteRenderer weaponSprite = weaponHolder.GetComponent<SpriteRenderer>();
        weaponSprite.sprite = currentWeapon.weaponSprite;
    }
}
