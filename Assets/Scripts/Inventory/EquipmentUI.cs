using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

    [SerializeField] private Transform _slotsParent;

    private EquipmentSlotUI[] _slots;
    private EquipmentManager _equipmentManager;
    private Equipment[] _currentEquipment;


    private void Start() {
        _equipmentManager = EquipmentManager.instance;

        _equipmentManager.onEquipmentChanged += UpdateUI;
        _slots = _slotsParent.GetComponentsInChildren<EquipmentSlotUI>();

        UpdateUI();
    }


    private void UpdateUI(Equipment newItem, Equipment oldItem) {
        _currentEquipment = _equipmentManager.GetCurrentEquipment();

        if (newItem != null) {
            _slots[(int)newItem.equipSlot].SetSlot(newItem.icon, newItem);
        }
        if (newItem == null) {
            _slots[(int)oldItem.equipSlot].ResetSlot();
        }
    }

    private void UpdateUI() {
        _currentEquipment = _equipmentManager.GetCurrentEquipment();
        if (_currentEquipment == null) return;

        for (int i = 0; i < _currentEquipment.Length; i++) {
            if (_currentEquipment[i] == null) {
                _slots[i].ResetSlot();
                continue;
            }

            _slots[i].SetSlot(_currentEquipment[i].icon, _currentEquipment[i]);
        }
    }


    public void Unequip(int index) {
        _equipmentManager.Unequip(index);
    }



    private void OnDestroy() {
        _equipmentManager.onEquipmentChanged -= UpdateUI;
    }


}
