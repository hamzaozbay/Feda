using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment {
    
    public float range;
    public float attackSpeed;
    public AnimatorOverrideController animController;
    public AnimationClip[] attackClips;

}
