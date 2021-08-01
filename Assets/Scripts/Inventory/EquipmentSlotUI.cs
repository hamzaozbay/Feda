using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlotUI : MonoBehaviour {

    [SerializeField] private Image _icon;
    [SerializeField] private EquipmentSlot _equipmentSlot;

    private Item _item;



    public void SetSlot(Sprite icon, Item item) {
        this._icon.sprite = icon;
        this._item = item;
        this._icon.enabled = true;
    }

    public void ResetSlot() {
        this._icon.enabled = false;
    }

    public Item GetItem() {
        return _item;
    }


    public EquipmentSlot EquipmentSlot { get { return _equipmentSlot; } }

}
