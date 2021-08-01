using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public SkinnedMeshRenderer targetMesh;
    public Transform rightHand;
    public GameObject trail;



    private void Awake() {
        GameManager.instance.SetPlayer(this.gameObject);
        EquipmentManager.instance.SetTargetMeshAndRightHand(targetMesh, rightHand);
        EquipmentManager.instance.EquipEquipments();
    }



}
