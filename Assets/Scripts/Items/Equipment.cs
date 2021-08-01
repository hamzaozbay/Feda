using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    public int damageModifier;
    public int armorModifier;


    public override void Use() {
        base.Use();

        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }


}

public enum EquipmentSlot { Head, Body, Legs, Weapon }
public enum EquipmentMeshRegion { Legs, Body, Arms }