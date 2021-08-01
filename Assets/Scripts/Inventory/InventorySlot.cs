using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {

    [SerializeField] private Image _icon;

    private Item _item;



    public void AddItem(Item newItem) {
        _item = newItem;

        _icon.sprite = _item.icon;
        _icon.enabled = true;
    }


    public void ClearSlot() {
        _item = null;

        _icon.sprite = null;
        _icon.enabled = false;
    }


    public void OnRemoveButton() {
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 pos = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        GameObject itemObj = Instantiate(_item.pickupPrefab, pos, Quaternion.identity);
        itemObj.transform.rotation = Quaternion.Euler(itemObj.transform.rotation.x, Random.Range(-180, 180), itemObj.transform.rotation.z);

        Inventory.instance.Remove(_item);
    }


    public void UseItem() {
        if (_item != null) {
            _item.Use();
        }
    }


    public Item GetItem() {
        return _item;
    }


}
