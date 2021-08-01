using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentTabLogic : MonoBehaviour {

    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName, _damageValue, _armorValue;
    [SerializeField] private GameObject _unequipButton, _damageValueGameObject, _armorValueGameobject;
    
    private EquipmentSlotUI _currentSlot;



    public void SelectSlot(EquipmentSlotUI slot) {
        _currentSlot = slot;

        if (_currentSlot != null) {
            FillFields();
        }
    }


    public void FillFields() {
        _itemIcon.sprite = _currentSlot.GetItem().icon;
        _itemIcon.enabled = true;
        _itemName.text = _currentSlot.GetItem().itemName;

        if (_currentSlot.GetItem() is Equipment) {
            Equipment equipment = (Equipment)_currentSlot.GetItem();
            _damageValue.text = equipment.damageModifier.ToString();
            _armorValue.text = equipment.armorModifier.ToString();

            _damageValueGameObject.SetActive(true);
            _armorValueGameobject.SetActive(true);
        }

        _unequipButton.SetActive(true);
    }
    public void ClearFields() {
        _itemIcon.sprite = null;
        _itemIcon.enabled = false;
        _itemName.text = null;

        _damageValue.text = null;
        _armorValue.text = null;
        _damageValueGameObject.SetActive(false);
        _armorValueGameobject.SetActive(false);

        _unequipButton.SetActive(false);
    }

    public void Unequip() {
        EquipmentManager.instance.Unequip((int)_currentSlot.EquipmentSlot);
        ClearFields();
    }

}
