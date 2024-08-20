using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefab; //Các prefab của các wepaon
    [SerializeField] Transform defaultWeaponSlot; //Transform ban đầu nếu không tìm được transform của weapon slot để đặt vũ khí (để hoạt ảnh, vị trí cầm weapon hợp lý)
    [SerializeField] Transform[] weaponSlots; //Transform của từng weapon slot
    List<Weapon> weapons = new List<Weapon>(); //Danh sách vũ khi của người chơi (add vào khi init)

    int currentWeaponIndex = 0; //Index của vũ khí hiện tại
    private void Start()
    {
        InitializeWeapon(); //Thực hiện khởi tạo vũ khí

    }

    private void InitializeWeapon()
    {
        foreach (var weapon in initialWeaponPrefab) // duyệt qua từng prefab được gán ở Hierachy
        {
            Transform weaponSlot = defaultWeaponSlot; //Đặt vị trí gắn của weapon là vị trí mặc định 
            foreach (var slot in weaponSlots) //Với mỗi slot của từng vũ khí
            {
                if (slot.gameObject.tag.Equals(weapon.GetAttachSlotTag())) // Nếu slot này trùng tag với tag được đặt sẵn của từng prefab weapon 
                {
                    weaponSlot = slot; //Gán slot gắn weapon vào vị trí đã cài đặt của từng wepon 
                    break;
                }
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot); //Tạo weapon;
            newWeapon.Init(gameObject); //Thực hiện gán owner của weapon vào player và unequip weapon vừa tạo
            weapons.Add(newWeapon); //add wapon vào danh sách weapon
        }
        EquipWeapon(0); // Thực hiện equip vũ khí ở index 0;
    }

    public void NextWeapon() // Equip vũ khí tiếp theo 
    {
        int nextWeaponIndex = (currentWeaponIndex+1)%weapons.Count;    
        EquipWeapon(nextWeaponIndex);
    }
    public Weapon GetActiveWeapon()
    {
        return weapons[currentWeaponIndex]; //Lấy ra weapon đang sử dụng (để xài các func trong script weapon của vũ khí đang dùng)
    }

    private void EquipWeapon(int weaponIndex) //Hàm equip vũ khi theo index cho trước
    {
        if (weaponIndex >= weapons.Count || weaponIndex < 0) // hàm check lỗi index
        {
            return;
        }
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].Unequip(); //Thực hiện unequip tất cả các vũ khí khác (aka deactive vũ khí)
        }
        weapons[weaponIndex].Equip(); // Equip vũ khí (aka active vũ khí)
        currentWeaponIndex = weaponIndex;  //chuyển index vũ khí hiện tại thành index vừa equip.
    }
}
