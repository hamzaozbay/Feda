using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    [SerializeField] private Item item;



    public override void Interact() {
        base.Interact();

        Pickup();
    }


    void Pickup() {
        //Debug.Log("pickuped " + item.itemName);

        bool pickedUp = Inventory.instance.Add(item);

        if (pickedUp)
            Destroy(gameObject);
    }

}