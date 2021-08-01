using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private List<GameObject> _tabs;
    [SerializeField] private TextMeshProUGUI _header;

    private InventorySlot[] _slots;
    private Inventory _inventory;




    private void Start() {
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallBack += UpdateUI;

        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }



    private void UpdateUI() {
        for (int i = 0; i < _slots.Length; i++) {
            if (i < _inventory.Items.Count) {
                _slots[i].AddItem(_inventory.Items[i]);
            }
            else {
                _slots[i].ClearSlot();
            }
        }
    }



    public void ChangeTab(GameObject tab) {
        foreach (GameObject g in _tabs) {
            if (g == tab) {
                g.SetActive(true);
            }
            else {
                g.SetActive(false);
            }
        }
    }
    public void ChangeHeader(string name) {
        _header.text = name;
    }


    private void OnDestroy() {
        _inventory.onItemChangedCallBack -= UpdateUI;
    }


}
