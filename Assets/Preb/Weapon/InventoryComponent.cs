using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefab;
    [SerializeField] Transform defaultWeaponSlot;
    [SerializeField] Transform[] weaponSlots;
    List<Weapon> weapons = new List<Weapon>();

    int currentWeaponIndex = 0;
    private void Start()
    {
        InitializeWeapon();

    }

    private void InitializeWeapon()
    {
        foreach (var weapon in initialWeaponPrefab)
        {
            Transform weaponSlot = defaultWeaponSlot;
            foreach (var slot in weaponSlots)
            {
                if (slot.gameObject.tag.Equals(weapon.GetAttachSlotTag()))
                {
                    weaponSlot = slot;
                    break;
                }
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            newWeapon.Init(gameObject);
            weapons.Add(newWeapon);
        }
        EquipWeapon(0);
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = (currentWeaponIndex+1)%weapons.Count;     
        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex >= weapons.Count || weaponIndex < 0)
        {
            return;
        }
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].Unequip();
        }
        weapons[weaponIndex].Equip();
        currentWeaponIndex = weaponIndex;
    }
}
