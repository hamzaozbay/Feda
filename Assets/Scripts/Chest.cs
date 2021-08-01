using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

    [SerializeField] private Animator _anim;
    [SerializeField] private Transform[] _dropSpots = new Transform[3];
    [SerializeField] private List<GameObject> _items = new List<GameObject>();



    public override void Interact() {
        base.Interact();

        _anim.SetTrigger("Open");
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(DropItems());
    }
    private IEnumerator DropItems() {
        yield return new WaitForSeconds(.75f);

        int howMany = Random.Range(1, _dropSpots.Length);
        for (int i = 0; i < howMany; i++) {
            Instantiate(_items[Random.Range(0, _items.Count)], _dropSpots[i].position, Quaternion.identity);
        }
    }
}
