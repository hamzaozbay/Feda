using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeSlot : MonoBehaviour {

    [SerializeField] private Image _icon;

    private Item _item;



    public void AddItem(Item item) {
        this._item = item;

        _icon.sprite = this._item.icon;
        _icon.enabled = true;
    }

    public void ClearSlot() {
        _item = null;

        _icon.sprite = null;
        _icon.enabled = false;
    }


    public Item GetItem() {
        return _item;
    }

}
