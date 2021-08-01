using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryTabLogic : MonoBehaviour {

    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName, _itemDescription, _damageValue, _armorValue;
    [SerializeField] private GameObject _useButton, _removeButton, _damageValueGameObject, _armorValueGameobject;

    private InventorySlot _currentSlot;



    public void SelectSlot(InventorySlot slot) {
        _currentSlot = slot;

        if (_currentSlot != null) {
            FillFields();
        }
        else {
            ClearFields();
        }
    }


    public void FillFields() {
        _itemIcon.sprite = _currentSlot.GetItem().icon;
        _itemIcon.enabled = true;
        _itemName.text = _currentSlot.GetItem().itemName;
        _itemDescription.text = _currentSlot.GetItem().itemDescription;

        if (_currentSlot.GetItem() is Equipment) {
            Equipment equipment = (Equipment)_currentSlot.GetItem();
            _damageValue.text = equipment.damageModifier.ToString();
            _armorValue.text = equipment.armorModifier.ToString();

            _damageValueGameObject.SetActive(true);
            _armorValueGameobject.SetActive(true);
        }

        _useButton.SetActive(true);
        _removeButton.SetActive(true);
    }
    public void ClearFields() {
        _itemIcon.sprite = null;
        _itemIcon.enabled = false;
        _itemName.text = null;
        _itemDescription.text = null;

        _damageValue.text = null;
        _armorValue.text = null;
        _damageValueGameObject.SetActive(false);
        _armorValueGameobject.SetActive(false);

        _useButton.SetActive(false);
        _removeButton.SetActive(false);
    }



    public void UseItem() {
        _currentSlot.UseItem();
        ClearFields();
    }
    public void RemoveItem() {
        _currentSlot.OnRemoveButton();
        ClearFields();
    }



}
