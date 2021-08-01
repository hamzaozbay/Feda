using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    
    public string itemName = "New Item";
    public string itemDescription = "item description.";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject pickupPrefab;
    public int soulValue = 0;

    

    public virtual void Use() {

        //Debug.Log("Using " + itemName); 
    }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }


}
