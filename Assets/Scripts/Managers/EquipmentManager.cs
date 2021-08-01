using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region  Singleton
    public static EquipmentManager instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }
    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    [SerializeField] private Equipment[] _currentEquipment;

    private Transform _rightHand;
    private SkinnedMeshRenderer _targetMesh;
    private SkinnedMeshRenderer[] _currentMeshes;
    private Inventory _inventory;



    private void Start() {
        _inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
        _currentMeshes = new SkinnedMeshRenderer[numSlots];
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.U)) {
        //     UnequipAll();
        // }
    }


    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        _currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);

        if (newItem is Weapon) {
            newMesh.transform.parent = _targetMesh.transform;
            newMesh.rootBone = _rightHand;
        }
        else {
            newMesh.transform.parent = _targetMesh.transform;
            newMesh.bones = _targetMesh.bones;
            newMesh.rootBone = _targetMesh.rootBone;
        }

        _currentMeshes[slotIndex] = newMesh;
    }


    public Equipment Unequip(int slotIndex) {
        if (_currentEquipment[slotIndex] == null) return null;

        if (_currentMeshes[slotIndex] == null) return null;


        Destroy(_currentMeshes[slotIndex].gameObject);

        Equipment oldItem = _currentEquipment[slotIndex];
        SetEquipmentBlendShapes(oldItem, 0);
        if (!_inventory.Add(oldItem)) {
            DropItem(oldItem);
        }

        _currentEquipment[slotIndex] = null;

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(null, oldItem);
        }

        return oldItem;
    }


    public void UnequipAll() {
        for (int i = 0; i < _currentEquipment.Length; i++) {
            Unequip(i);
        }
    }


    private void DropItem(Item item) {
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 pos = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        GameObject itemObj = Instantiate(item.pickupPrefab, pos, Quaternion.identity);
        itemObj.transform.rotation = Quaternion.Euler(itemObj.transform.rotation.x, Random.Range(-180, 180), itemObj.transform.rotation.z);
    }


    private void SetEquipmentBlendShapes(Equipment item, int weight) {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
            _targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }



    public void SetTargetMeshAndRightHand(SkinnedMeshRenderer targetMesh, Transform rightHand) {
        this._targetMesh = targetMesh;
        this._rightHand = rightHand;
    }

    public void EquipEquipments() {
        foreach (Equipment e in _currentEquipment) {
            if (e == null) continue;

            if (e is Weapon) {
                SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(e.mesh);
                newMesh.transform.parent = _targetMesh.transform;
                newMesh.rootBone = _rightHand;
                _currentMeshes[(int)e.equipSlot] = newMesh;
            }
            else {
                SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(e.mesh);
                newMesh.transform.parent = _targetMesh.transform;
                newMesh.bones = _targetMesh.bones;
                newMesh.rootBone = _targetMesh.rootBone;

                _currentMeshes[(int)e.equipSlot] = newMesh;
            }

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(e, null);
            }
        }
    }



    public Equipment[] GetCurrentEquipment() {
        return _currentEquipment;
    }

}
