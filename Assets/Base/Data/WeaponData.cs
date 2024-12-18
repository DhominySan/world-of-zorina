using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
   [Header("Weapon Info")]
   public int weaponID;
   public string weaponName;
   public Sprite weaponSprite;

   [Header("Stats")]
   public int weaponDamage;
      
}
