using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    [SerializeField] private int _space = 9;
    [SerializeField] private List<Item> _items = new List<Item>();



    public bool Add(Item item) {
        if (!item.isDefaultItem) {
            if (_items.Count >= _space) {
                return false;
            }

            _items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove(Item item) {
        _items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();

    }

    public void ClearInventory() {
        _items.Clear();
    }


    public List<Item> Items { get { return _items; } }

}
